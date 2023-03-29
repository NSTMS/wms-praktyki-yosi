using System.ComponentModel.DataAnnotations;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Enitities
{
    public class ProductLocations
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ShelfId { get; set; }
        public int Quantity { get; set; } // na sztywno
        public string Tag { get; set; }
        public virtual Product Product { get; set; }
        public virtual Shelf Shelf { get; set; }
    }
}
 