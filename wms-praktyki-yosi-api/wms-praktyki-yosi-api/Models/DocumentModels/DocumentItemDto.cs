namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class DocumentItemDto
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Position { get; set; }
        public bool Arriving { get; set; }
        public int QuantityPlaned { get; set; }
        public int QuantityDone { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
    }
}
