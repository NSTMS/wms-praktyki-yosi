using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models.StandingOrderModels
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public bool Arriving { get; set; }
        public int Quantity { get; set; }
        public string? Tag { get; set; }
    }
}
