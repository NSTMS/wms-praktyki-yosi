using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Enitities
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string ProductName { get; set; }
        [MaxLength(13)]
        [MinLength(13)]
        public string EAN { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}