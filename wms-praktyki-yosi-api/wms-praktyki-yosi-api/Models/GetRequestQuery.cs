using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Models
{
    public class GetRequestQuery
    {
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }
        public bool Descending { get; set; } = false;

    }
}
