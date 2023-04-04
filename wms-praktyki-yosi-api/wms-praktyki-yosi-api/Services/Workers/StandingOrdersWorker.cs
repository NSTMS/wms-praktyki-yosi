using Microsoft.EntityFrameworkCore;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models.DocumentModels;

namespace wms_praktyki_yosi_api.Services.Workers
{
    public class StandingOrdersWorker : BackgroundService
    {
        private readonly ILogger<MagzineStateWorker> _logger;
        private readonly IServiceProvider _provider;

        public StandingOrdersWorker(ILogger<MagzineStateWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _provider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _provider.CreateScope())
                {
                    _logger.LogInformation("{Dt}: Starting to check for Orders to fulfill... ", DateTime.Now);
                    var context = scope.ServiceProvider.GetRequiredService<MagazinesDbContext>();
                    var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();

                    var ordersToFulfill = context.StandingOrders
                        .Where(s => s.NextOrder < DateTime.Now && !s.Deleted && s.Status != "Stopped")
                        .ToList();

                    if (!ordersToFulfill.Any())
                    {
                        _logger.LogInformation("{Dt}: No Orders To fulfill.", DateTime.Now);
                        await Task.Delay(10000, stoppingToken);
                        continue;  
                    }
                    _logger.LogInformation("{Dt}: Starting to make documents from orders to fulfill...", DateTime.Now);
                    long startingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    foreach (var order in ordersToFulfill)
                    {
                        var itemsToAdd = context.OrderItems
                            .Where(i => i.StandingOrderId == order.Id)
                            .Include(i => i.Product)
                            .Select(i => new AddDocumentItemDto()
                            {
                                ProductName = i.Product.ProductName,
                                Arriving = i.Arriving,
                                Quantity = i.Quantity,
                                Tag = i.Tag
                            })
                            .ToList();

                        var documentToAdd = new AddDocumentDto()
                        {
                            Date = DateTime.Now,
                            Client = order.Client,
                            MagazineId = order.MagazineId,
                            ItemList = itemsToAdd
                        };
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                documentService.AddDocument(documentToAdd);
                                order.Status = "Working";
                                order.NextOrder = order.NextOrder.AddMilliseconds(order.Interval);
                                context.SaveChanges();
                                break;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning("{Dt}: Cannot create document due to '{s}' error. Try: {i}\n\tRetryig...", DateTime.Now, ex.Message, i + 1);
                                if (i == 4)
                                {
                                    _logger.LogError("{Dt}: Cannot create document due to '{s}' error. Stopped Trying.", DateTime.Now, ex.Message);
                                    order.Status = "Stopped";
                                    order.Message = $"Order stopped due to error '{ex.Message}'";
                                    context.SaveChanges(); 
                                }
                            }
                        }
                        
                    }

                    var timeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startingTime;
                    _logger.LogInformation("{DT}: Finished adding documents. Time elapsed: {i}ms", DateTime.Now, timeElapsed);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
