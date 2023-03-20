using AutoMapper;
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
       },
       new Product {
         Id = 11,
           ProductName = "Kamera internetowa Logitech",
           EAN = "471385937",
           Price = 99.99,
           Quantity = 265
       },

       new Product {
         Id = 12,
           ProductName = "Klawiatura i mysz bezprzewodowa Microsoft",
           EAN = "231487970",
           Price = 69.99,
           Quantity = 879
       }

       ,
       new Product {
         Id = 13,
           ProductName = "Smartwatch Samsung Galaxy",
           EAN = "547918380",
           Price = 329.99,
           Quantity = 467
       }

       ,
       new Product {
         Id = 14,
           ProductName = "Słuchawki Beats by Dr. Dre",
           EAN = "728149240",
           Price = 199.99,
           Quantity = 534
       }

       ,
       new Product {
         Id = 15,
           ProductName = "Laptop Dell Inspiron",
           EAN = "889452368",
           Price = 849.99,
           Quantity = 342
       }

       ,
       new Product {
         Id = 16,
           ProductName = "Konsola do gier Sony PlayStation",
           EAN = "124576893",
           Price = 499.99,
           Quantity = 721
       }

       ,
       new Product {
         Id = 17,
           ProductName = "Oprogramowanie Microsoft Office",
           EAN = "436789565",
           Price = 149.99,
           Quantity = 925
       }

       ,
       new Product {
         Id = 18,
           ProductName = "Telewizor LG OLED",
           EAN = "237894611",
           Price = 2499.99,
           Quantity = 123
       }

       ,
       new Product {
         Id = 19,
           ProductName = "Drukarka HP OfficeJet",
           EAN = "901238475",
           Price = 149.99,
           Quantity = 654
       }

       ,
       new Product {
         Id = 20,
           ProductName = "Głośniki Logitech Surround Sound",
           EAN = "568274902",
           Price = 199.99,
           Quantity = 462
       }

       ,
       new Product {
         Id = 21,
           ProductName = "Smartfon Apple iPhone",
           EAN = "456321789",
           Price = 1099.99,
           Quantity = 217
       }

       ,
       new Product {
         Id = 22,
           ProductName = "Karta dźwiękowa Asus Xonar",
           EAN = "786543290",
           Price = 129.99,
           Quantity = 823
       }

       ,
       new Product {
         Id = 23,
           ProductName = "Mysz gamingowa Razer DeathAdder",
           EAN = "127894356",
           Price = 79.99,
           Quantity = 665
       }

       ,
       new Product {
         Id = 24,
           ProductName = "Router WiFi Netgear",
           EAN = "674832190",
           Price = 199.99,
           Quantity = 439
       }

       ,
       new Product {
         Id = 25,
           ProductName = "Tablet Samsung Galaxy Tab",
           EAN = "972341658",
           Price = 449.99,
           Quantity = 567
       }

       ,
       new Product {
         Id = 26,
           ProductName = "Klawiatura Logitech Illuminated",
           EAN = "137854209",
           Price = 79.99,
           Quantity = 745
       },
       new Product {
         Id = 27,
           ProductName = "Monitor Dell UltraSharp",
           EAN = "732145987",
           Price = 699.99,
           Quantity = 321
       }

       ,
       new Product {
         Id = 28,
           ProductName = "Karta graficzna Nvidia GeForce",
           EAN = "876549013",
           Price = 399.99,
           Quantity = 598
       }

       ,
       new Product {
         Id = 29,
           ProductName = "Słuchawki Sony WH-1000XM4",
           EAN = "290437561",
           Price = 349.99,
           Quantity = 431
       }

       ,
       new Product {
         Id = 30,
           ProductName = "Oprogramowanie Adobe Creative Cloud",
           EAN = "658943120",
           Price = 599.99,
           Quantity = 172
       }
    };
        private readonly MagazinesDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(MagazinesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddNewProduct(ProductDto dto) {
            
            var product = _mapper.Map<Product>( dto );
            _context.Products
                .Add( product );
            _context.SaveChanges();
            return product.Id;
            
        }
        public bool RemoveProduct(int id) {
            var prod =_context
                .Products.
                FirstOrDefault(r => r.Id == id);
            if (prod == null)
            {
                return false;
            }

            _context
                 .Products
                 .Remove(prod);

            _context.SaveChanges();
            return true;
        }
        public bool UpdateProduct(int id, ProductDto dto) {
            var prod = _context.Products.FirstOrDefault(r => r.Id == id);

            if(prod == null) {
                return false;
            }

            prod.ProductName = dto.ProductName;
            prod.EAN = dto.EAN;
            prod.Price = dto.Price;
            prod.Quantity = dto.Quantity;
            _context.SaveChanges();

            return true;
        }
        public IEnumerable<Product> GetAll()
        {
            //var seeder = new MagazinesSeeder(_context);
            //seeder.Seed();
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(r => r.Id == id);
            if (product == null)
                return null;
            return product;
        }

    }
}
