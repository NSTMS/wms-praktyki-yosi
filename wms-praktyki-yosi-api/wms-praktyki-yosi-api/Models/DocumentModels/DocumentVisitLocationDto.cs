using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class DocumentVisitLocationDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? Tag { get; set; }
    }
}
