namespace wms_praktyki_yosi_api.Models.StandingOrderModels
{
    public class EditOrderItemDto
    {
        public string? Tag { get; set; }
        public bool Arriving { get; set; }
        public int Quantity { get; set; }
    }
}
