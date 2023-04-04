namespace wms_praktyki_yosi_api.Enitities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid StandingOrderId { get; set; }
        public virtual StandingOrder StandingOrder { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public bool Arriving { get; set; }
        public int Quantity { get; set; }
        public string? Tag { get; set; }
        public byte[] Version { get; set; }
    }
}
