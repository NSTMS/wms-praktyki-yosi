using System.ComponentModel.DataAnnotations;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models.DocumentModels
{
    public class AddDocumentDto
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        [Required]
        public int MagazineId { get; set; }
        public List<AddDocumentItemDto> ItemList { get; set; }

    }
}
