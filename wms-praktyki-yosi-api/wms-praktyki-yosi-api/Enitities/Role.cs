using System.ComponentModel.DataAnnotations;

namespace wms_praktyki_yosi_api.Enitities
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
