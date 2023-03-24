using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface IProductService
    {
        int AddNewProduct(ProductDto dto);
        void RemoveProduct(int id);
        void UpdateProduct(int id, ProductDto dto);
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
    }
}