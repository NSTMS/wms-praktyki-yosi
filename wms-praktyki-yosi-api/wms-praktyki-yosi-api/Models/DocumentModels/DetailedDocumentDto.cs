namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class DetailedDocumentDto
    {
        public DateTime Date { get; set; }
        public int MagazineId { get; set; }
        public string Client { get; set; }
        public int TotalQuantity { get; set; }
        public int QuantityDone { get; set; }
        public bool Finished { get; set; }
        public List<DocumentItemDto> Items { get; set; }
    }
}
