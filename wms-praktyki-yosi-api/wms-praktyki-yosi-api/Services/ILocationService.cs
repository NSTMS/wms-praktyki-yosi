using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface ILocationService
    {
        public int AddProductToLocation(ProductLocationDto location);
        bool DeleteLocation(int id);
        IEnumerable<ProductLocationDto> GetAllLocations();
        ReturnProductLocationDto GetLocationById(int id);
        bool UpdateLocation(int id, ProductLocationDto location);
    }
}   