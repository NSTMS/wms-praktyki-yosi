using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.ProductModels;

namespace wms_praktyki_yosi_api.Services
{
    public interface ILocationService
    {
        public int AddProductToLocation(ProductLocationDto location);
        void DeleteLocation(int id);
        IEnumerable<ReturnProductLocationDto> GetAllLocations(GetRequestQuery query);
        ReturnProductLocationDto GetLocationById(int id);
        void UpdateLocation(int id, EditProductLocationDto location);
    }
}