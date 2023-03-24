namespace wms_praktyki_yosi_api.Enitities
{
    public class Document
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public bool Finished { get; set; }
        public int MagazineId { get; set; }
        public virtual Magazine Magazine { get; set; }
        public virtual List<DocumentItem> Items { get; set; }
    }
}
