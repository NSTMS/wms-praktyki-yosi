using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface IMagazineService
    {
        int AddMagzine(MagazineDto dto);
        bool DeleteMagazine(int id);
        List<Magazine> GetAll();
        MagazineDto GetById(int id);
        List<ProductLocationDto> GetLocationsInMagazine(int id);
        public ProductDto GetProductInMagazine(int id, int productId);
        List<ProductDto> GetProductsInMagazine(int id);
        bool UpdateMagazine(int id, MagazineDto dto);
        public List<ReturnProductLocationDto> GetLocationsOfProduct(int id, int productId);
    }
}