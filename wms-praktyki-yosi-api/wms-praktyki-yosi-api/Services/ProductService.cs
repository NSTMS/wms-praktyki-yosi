using Microsoft.AspNetCore.Http.HttpResults;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>() {
               new Product {
                    Id = 1,
                    ProductName = "Laptop Lenovo ThinkPad",
                    EAN = "471385936",
                    Price = 1499.99,
                    Quantity = 435
                },
                new Product {
                    Id = 2,
                    ProductName = "Smartphone Samsung Galaxy",
                    EAN = "231487969",
                    Price = 799.99,
                    Quantity = 744
                },
                new Product {
                    Id = 3,
                    ProductName = "Monitor LG UltraFine",
                    EAN = "547918379",
                    Price = 1299.00,
                    Quantity = 921
                },
                new Product {
                    Id = 4,
                    ProductName = "Tablet Apple iPad",
                    EAN = "728149239",
                    Price = 599.99,
                    Quantity = 236
                },
                new Product {
                    Id = 5,
                    ProductName = "Głośnik Bluetooth JBL",
                    EAN = "889452367",
                    Price = 249.99,
                    Quantity = 534
                },
                new Product {
                    Id = 6,
                    ProductName = "Klawiatura Mechaniczna Corsair",
                    EAN = "124576892",
                    Price = 199.99,
                    Quantity = 678
                },
                new Product {
                    Id = 7,
                    ProductName = "Mysz Bezprzewodowa Logitech",
                    EAN = "436789564",
                    Price = 89.99,
                    Quantity = 784
                },
                new Product {
                    Id = 8,
                    ProductName = "Karta Graficzna Nvidia GeForce",
                    EAN = "237894610",
                    Price = 699.99,
                    Quantity = 319
                },
                new Product {
                    Id = 9,
                    ProductName = "Router WiFi TP-Link",
                    EAN = "901238474",
                    Price = 119.99,
                    Quantity = 873
                },
                new Product {
                    Id = 10,
                    ProductName = "Słuchawki Bezprzewodowe Sony",
                    EAN = "568274901",
                    Price = 299.99,
                    Quantity = 167
                }
            };

        public void AddNewProduct(ProductDto dto) {
            _products.Add(
                new Product
                {
                    Id = _products.Count + 1,
                    ProductName = dto.ProductName,
                    EAN = dto.EAN,
                    Price = dto.Price,
                    Quantity = dto.Quantity
                }
               );
        }
        public bool RemoveProduct(int id) {
            var prod = _products.FirstOrDefault(r => r.Id == id);
            if (prod == null)
            {
                return false;
            }
            _products.Remove(prod);
            return true;
        }
        public List<Product> UpdateProduct(int id, ProductDto product) {
            var prod = _products.FirstOrDefault(r => r.Id == id);

            if(prod == null) {
                return null;
            }
            prod.Id = id;
            prod.ProductName = product.ProductName;
            prod.EAN = product.EAN;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;

            return _products;
        }
        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
        public Product GetById(int id)
        {
            var product = _products.FirstOrDefault(r => r.Id == id);
            if (product == null) return null;
            else return product;
        }

    }
}
