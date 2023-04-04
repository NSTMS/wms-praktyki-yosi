using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.StandingOrderModels;

namespace wms_praktyki_yosi_api.Services
{
    public interface IStandingOrdersService
    {
        void AddItemToOrder(string id, AddOrderItemDto newItem);
        string AddStandingOrder(AddStandingOrderDto newOrder);
        void DeleteOrderItem(string itemId);
        void DeleteStandingOrder(string id);
        void EditOrderItem(string id, string ItemId, EditOrderItemDto newItem);
        void EditStandingOrder(string id, EditStandingOrderDto newOrder);
        IEnumerable<StandingOrderDto> GetAllOrders(GetRequestQuery query);
        DetailedStandingOrderDto GetById(string id);
        IEnumerable<OrderItemDto> GetItemsInOrder(string id, GetRequestQuery query);
    }
}