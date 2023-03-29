using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface IMagazineService
    {
        int AddMagzine(MagazineDto dto);
        void DeleteMagazine(int id);
        IEnumerable<ReturnMagazineDto> GetAll(GetRequestQuery query);
        ReturnMagazineDto GetById(int id);
        List<ProductLocationDto> GetLocationsInMagazine(int id);
        public ProductDto GetProductInMagazine(int id, int productId);
        List<ProductDto> GetProductsInMagazine(int id);
        void UpdateMagazine(int id, EditMagazineDto dto);
        public List<ReturnProductLocationDto> GetLocationsOfProduct(int id, int productId);
    }
}