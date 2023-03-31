using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class EditDocumentItemDto
    {
        public int Quantity { get; set; }
        public string? Tag { get; set; }
    }
}
