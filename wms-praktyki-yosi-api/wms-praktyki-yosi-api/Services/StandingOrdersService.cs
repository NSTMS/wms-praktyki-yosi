using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Models.StandingOrderModels;
using wms_praktyki_yosi_api.Services.Static;
using static iTextSharp.text.pdf.AcroFields;

namespace wms_praktyki_yosi_api.Services
{
    public class StandingOrdersService : IStandingOrdersService
    {
        private readonly MagazinesDbContext _context;
        private readonly IMapper _mapper;

        public StandingOrdersService(MagazinesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<StandingOrderDto> GetAllOrders(GetRequestQuery query)
        {
            var orders = _context.StandingOrders
                .Where(o => !o.Deleted && (query.SearchTerm == null || o.Client.ToLower().Contains(query.SearchTerm.ToLower())
                                                                    || o.Status.ToLower().Contains(query.SearchTerm.ToLower())
                                                                    || (o.Message != null && o.Message.ToLower().Contains(query.SearchTerm.ToLower()))))
                .Include(o => o.Items)
                .ToList();

            var orderDtos = _mapper.Map<List<StandingOrderDto>>(orders).AsQueryable();

            if (query.OrderBy != null)
            {
                try
                {
                    var selectedColumn = OrderByColumnSelectors.Orders[query.OrderBy.ToLower()];
                    orderDtos = (query.Descending)
                        ? orderDtos.OrderByDescending(selectedColumn)
                        : orderDtos.OrderBy(selectedColumn);
                }
                catch (KeyNotFoundException)
                {
                    throw new BadRequestException("Bad Column Name");
                }
                
            }

            return orderDtos.ToList();
        }
        public string AddStandingOrder(AddStandingOrderDto newOrder)
        {
            var orderToAdd = new StandingOrder()
            {
                Interval = newOrder.Interval,
                NextOrder = DateTime.Now.AddMilliseconds(newOrder.Interval),
                Client = newOrder.Client,
                MagazineId = newOrder.MagazineId,
                Deleted = false,
                Status = "NotStarted",

            };

            var itemsToAdd = newOrder.Items
                .Select(i => new OrderItem()
                {
                    ProductId = GetProductIdFromName(i.ProductName),
                    Arriving = i.Arriving,
                    Quantity = i.Quantity,
                    Tag = i.Tag,
                })
                .ToList();

            orderToAdd.Items = itemsToAdd;

            _context.StandingOrders.Add(orderToAdd);
            ConcurencyResolver.SafeSave(_context);

            return orderToAdd.Id.ToString();
        }
        public DetailedStandingOrderDto GetById(string id)
        {
            var order = GetOrderById(id);

            order.Items = _context.OrderItems
                .Include(i => i.Product)
                .Where(i => i.StandingOrderId.ToString() == id)
                .ToList();

            var orderDto = _mapper.Map<DetailedStandingOrderDto>(order);

            return orderDto;
        }
        public void EditStandingOrder(string id, EditStandingOrderDto newOrder)
        {
            var order = GetOrderById(id);

            order.Interval = newOrder.Interval;
            order.Client = newOrder.Client;
            order.MagazineId = newOrder.MagazineId;

            ConcurencyResolver.SafeSave(_context);

        }
        public void DeleteStandingOrder(string id)
        {
            var order = GetOrderById(id);

            order.Deleted = true;
            ConcurencyResolver.SafeSave(_context);
        }

        public IEnumerable<OrderItemDto> GetItemsInOrder(string id, GetRequestQuery query)
        {
            var order = GetOrderById(id);

            var itemDtos = _context.OrderItems
                .Where(i => i.StandingOrderId == order.Id)
                .Where(i => query.SearchTerm == null || i.Product.ProductName.ToLower().Contains(query.SearchTerm.ToLower())
                                                     || (i.Tag != null && i.Tag.ToLower().Contains(query.SearchTerm.ToLower())))
                .Include(i => i.Product)
                .Select(i => _mapper.Map<OrderItemDto>(i))
                .ToList()
                .AsQueryable();

            if (query.OrderBy != null)
            {
                try
                {
                    var selectedColumn = OrderByColumnSelectors.OrderItems[query.OrderBy.ToLower()];
                    itemDtos = (query.Descending)
                        ? itemDtos.OrderByDescending(selectedColumn)
                        : itemDtos.OrderBy(selectedColumn);
                }
                catch (KeyNotFoundException)
                {
                    throw new BadRequestException("Bad Column Name");
                }

            }

            return itemDtos.ToList();

        }
        public void AddItemToOrder(string id, AddOrderItemDto newItem)
        {
            var order = GetOrderById(id);

            var productId = GetProductIdFromName(newItem.ProductName);
            
            var itemToAdd = new OrderItem()
            {
                StandingOrderId = order.Id,
                ProductId = productId,
                Arriving = newItem.Arriving,
                Quantity = newItem.Quantity,
                Tag = newItem.Tag,
            };

            _context.OrderItems.Add(itemToAdd);

            ConcurencyResolver.SafeSave(_context);
        }
        public void EditOrderItem(string id, string ItemId, EditOrderItemDto newItem)
        {
            var order = GetOrderById(id);

            var item = _context.OrderItems
                .FirstOrDefault(i => i.Id.ToString() == ItemId)
                ?? throw new NotFoundException("158");


            item.Arriving = newItem.Arriving;
            item.Quantity = newItem.Quantity;
            item.Tag = newItem.Tag;

            ConcurencyResolver.SafeSave(_context);
        }
        public void DeleteOrderItem(string itemId)
        {
            var item = _context.OrderItems
               .FirstOrDefault(i => i.Id.ToString() == itemId)
               ?? throw new NotFoundException("158");
            _context.OrderItems.Remove(item);

            ConcurencyResolver.SafeSave(_context);
        }

        // --------- private methods -----------
        private int GetProductIdFromName(string productName)
        {
            return (_context.Products
                .FirstOrDefault(p => p.ProductName == productName)
                ?? throw new NotFoundException("151")).Id;
        }
        private StandingOrder GetOrderById(string id)
        {
            return _context.StandingOrders
                .FirstOrDefault(o => o.Id.ToString() == id && !o.Deleted)
                ?? throw new NotFoundException("157");
        }
        private StandingOrder GetOrderWithItemsById(string id)
        {
            return _context.StandingOrders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id.ToString() == id && !o.Deleted)
                ?? throw new NotFoundException("157");
        }

    }
}
