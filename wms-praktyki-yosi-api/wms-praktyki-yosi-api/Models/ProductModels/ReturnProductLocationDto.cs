namespace wms_praktyki_yosi_api.Models.ProductModels
{
    public class ReturnProductLocationDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Position { get; set; }
        public int MagazineId { get; set; }
        public int Quantity { get; set; } // na sztywno
        public string Tag { get; set; }
    }
}
