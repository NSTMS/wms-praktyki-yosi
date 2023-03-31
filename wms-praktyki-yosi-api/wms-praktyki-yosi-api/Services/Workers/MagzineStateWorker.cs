using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

using wms_praktyki_yosi_api.Enitities;
using System.Reflection.Metadata;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services.Workers
{
    public class MagzineStateWorker : BackgroundService
    {
        private readonly int FIVE_MINUTES = 5 * 60 * 1000;
        private readonly ILogger _logger;
        private readonly IServiceProvider _provider;
        private readonly string[] productHeaders = {"Id", "ProductName", "EAN", "Price", "Quantity" };

        public MagzineStateWorker(ILogger<MagzineStateWorker> logger, IServiceProvider serviceProvider)
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
                    var context = scope.ServiceProvider.GetRequiredService<MagazinesDbContext>();
                    var magazineSevice = scope.ServiceProvider.GetRequiredService<IMagazineService>();

                    var magazineIds = context.Magazines
                        .Select(m => m.Id)
                        .ToList();

                    long startingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    _logger.LogInformation("{DT}: Starting generating report on Magzine state...", DateTime.Now);

                    using (var document = new iTextSharp.text.Document())
                    {
                        PdfWriter.GetInstance(
                            document,
                            new FileStream("Documents/MagazineStateOf"
                                           + DateTime.Now.ToString("dd-mm-yyyyTHH.mm.ss")
                                           + ".pdf", FileMode.Create));
                        document.Open();

                        foreach (var magazineId in magazineIds)
                        {
                            var timeMiddle = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startingTime;
                            _logger.LogInformation("{DT}: Document Created\nTime elapsed: {i}ms", DateTime.Now, timeMiddle);
                            var productsInMagazine = magazineSevice.GetProductsInMagazine(magazineId, new Models.GetRequestQuery());
                            var detailedMagazine = magazineSevice.GetById(magazineId);
                            timeMiddle = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startingTime;
                            _logger.LogInformation("{DT}: Downloaded data to cashe\nTime elapsed: {i}ms", DateTime.Now, timeMiddle);

                            var header = AddHeader(detailedMagazine);
                            var table = CreatePdfTable(productHeaders, productsInMagazine.Cast<object>().ToList());

                            document.Add(header);
                            document.Add(table);
                        }
                    }

                    var timeElapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startingTime;
                    _logger.LogInformation("{DT}: Finished report on magazines state\nTime elapsed: {i}ms", DateTime.Now, timeElapsed);
                    

                }
                await Task.Delay(FIVE_MINUTES, stoppingToken);
            }
        }

        private static PdfPTable CreatePdfTable(string[] headers, List<object> data)
        {
            // Create a table with the same number of columns as the number of headers
            PdfPTable table = new PdfPTable(headers.Length);

            // Set the width of the table to 100% of the page width
            table.WidthPercentage = 100;

            // Add headers to the table
            foreach (string header in headers)
            {
                table.AddCell(header);
            }

            // Add data to the table
            foreach (object item in data)
            {
                foreach (string header in headers)
                {
                    PropertyInfo propInfo = item.GetType().GetProperty(header);
                    table.AddCell(propInfo.GetValue(item).ToString());
                }
            }

           return table;
        }
        private static Paragraph AddHeader(ReturnMagazineDto data)
        {
            // Create a new paragraph for the header
            Paragraph header = new Paragraph();

            // Add the Name and Address to the header as a large, bold text
            Chunk nameChunk = new Chunk(data.Name, new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
            Chunk addressChunk = new Chunk(data.Address, new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            header.Add(nameChunk);
            header.Add("\n");
            header.Add(addressChunk);
            header.Add("\n\n");

            // Add the rest of the data to the header as smaller, normal text
            Chunk dimentionsChunk = new Chunk($"Dimensions: {data.Dimentions}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk shelvesPerRowChunk = new Chunk($"Shelves per Row: {data.ShelvesPerRow}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk maxShelfLoadChunk = new Chunk($"Max Shelf Load: {data.MaxShelfLoad}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk shelfNumberChunk = new Chunk($"Shelf Number: {data.ShelfNumber}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk totalCapacityChunk = new Chunk($"Total Capacity: {data.TotalCapacity}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk totalQuantityChunk = new Chunk($"Total Quantity: {data.TotalQuantity}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            Chunk freeSpaceChunk = new Chunk($"Free Space: {data.FreeSpace}", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            header.Add(dimentionsChunk);
            header.Add("    ");
            header.Add(shelvesPerRowChunk);
            header.Add("    ");
            header.Add(maxShelfLoadChunk);
            header.Add("    ");
            header.Add(shelfNumberChunk);
            header.Add("\n");
            header.Add(totalCapacityChunk);
            header.Add("    ");
            header.Add(totalQuantityChunk);
            header.Add("    ");
            header.Add(freeSpaceChunk);
            header.Add("\n\n");

            // Return the header
            return header;
        }
    }
}
