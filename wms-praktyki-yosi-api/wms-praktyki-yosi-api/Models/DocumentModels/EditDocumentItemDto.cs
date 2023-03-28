using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class EditDocumentItemDto
    {
        public bool Arriving { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Tag { get; set; }
    }
}
