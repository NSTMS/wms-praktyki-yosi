using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.ProductModels;

namespace wms_praktyki_yosi_api.Services
{
    public interface IProductService
    {
        int AddNewProduct(ProductDto dto);
        void RemoveProduct(int id);
        void UpdateProduct(int id, ProductDto dto);
        IEnumerable<ProductDto> GetAll(GetRequestQuery query);
        ProductDto GetById(int id);
        IEnumerable<ProductLocationDto> GetProductLocations(int id, GetRequestQuery query);
    }
}