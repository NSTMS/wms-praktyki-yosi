namespace wms_praktyki_yosi_api.Enitities
{
    public class StandingOrder
    {
        public Guid Id { get; set; }
        public long Interval { get; set; }
        public DateTime NextOrder { get; set; }
        public string Client { get; set; }
        public int MagazineId { get; set; }
        public virtual Magazine Magazine { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }
        public virtual List<OrderItem> Items { get; set; }
        public bool Deleted { get; set; }
        public byte[] Version { get; set; }

    }
}
