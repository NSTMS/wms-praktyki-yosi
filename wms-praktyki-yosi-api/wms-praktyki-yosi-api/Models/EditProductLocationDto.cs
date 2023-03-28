namespace wms_praktyki_yosi_api.Models
{
    public class EditProductLocationDto 
    {
        public int MagazineId { get; set; }
        public string Position { get; set; }
        public int Quantity { get; set; } // na sztywno
        public string Tag { get; set; }
    }
}
