using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class AddDocumentItemDto
    {
        [Required]
        public string ProductName { get; set; }
        public bool Arriving { get; set; } = false;
        [Required]
        public int Quantity { get; set; }
        public string Tag { get; set; }
    }
}