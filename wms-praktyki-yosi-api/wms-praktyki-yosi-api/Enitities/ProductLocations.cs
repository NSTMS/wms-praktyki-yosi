using System.ComponentModel.DataAnnotations;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Enitities
{
    public class ProductLocations
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ShelfId { get; set; }
        public int Quantity { get; set; } // na sztywno
        public virtual Product Product { get; set; }
        public virtual Shelves Shelves { get; set; }
    }
}
 