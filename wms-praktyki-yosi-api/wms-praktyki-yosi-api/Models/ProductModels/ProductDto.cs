using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace wms_praktyki_yosi_api.Models.ProductModels
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [MaxLength(13)]
        [MinLength(13)]
        public string EAN { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<ReturnProductLocationDto> Locations { get; set; }
    }
}
