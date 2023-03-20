using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface ILocationService
    {
        int AddNewLocation(ProductLocationDto location);
        bool DeleteLocation(int id);
        IEnumerable<ProductLocationDto> GetAllLocations();
        ProductLocations GetLocationById(int id);
        bool UpdateLocation(int id, ProductLocationDto location);
    }
}