using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models
{
    public class ProductLocationDto
    {
        public string ProductId { get; set; }
        public string ShelfId { get; set; }
        public int Quantity { get; set; } // na sztywno
    }
}