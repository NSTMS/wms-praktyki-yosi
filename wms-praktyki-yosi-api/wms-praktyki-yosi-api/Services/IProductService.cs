using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface IProductService
    {
        int AddNewProduct(ProductDto dto);
        bool RemoveProduct(int id);
        bool UpdateProduct(int id, ProductDto dto);
        IEnumerable<ProductDto> GetAll();
        Product GetById(int id);
    }
}