using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public interface IProductService
    {
        void AddNewProduct(ProductDto dto);
        bool RemoveProduct(int id);
        List<Product> UpdateProduct(int id, ProductDto product);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
    }
}