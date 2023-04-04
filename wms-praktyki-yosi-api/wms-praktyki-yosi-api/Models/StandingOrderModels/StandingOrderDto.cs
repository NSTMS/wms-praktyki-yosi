using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models.StandingOrderModels
{
    public class StandingOrderDto
    {
        public string Id { get; set; }
        public long Interval { get; set; }
        public DateTime NextOrder { get; set; }
        public string Client { get; set; }
        public int MagazineId { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }
        public int TotalQuantity { get; set; }
    }
}
