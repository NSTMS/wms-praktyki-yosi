namespace wms_praktyki_yosi_api.Enitities
{
    public class DocumentItem
    {
        public Guid Id { get; set; }
        public bool Arriving { get; set; }
        public string Tag { get; set; }
        public int Quantityplaned { get; set; }
        public int QuantityDone { get; set; }
        public int MagzineId { get; set; }
        public string Position { get; set; }
        public Guid DocumentId { get; set; }
        public int ProductId { get; set; }
        public virtual Document Document { get; set; }
        public virtual Product Product { get; set; }

        public byte[] Version { get; set; }
    }
}
