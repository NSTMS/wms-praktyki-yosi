namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int MagazineId { get; set; }
        public string Client { get; set; }
        public int TotalQuantity { get; set; }
        public int QuantityDone { get; set; }
        public bool Finished { get; set; }
    }
}
