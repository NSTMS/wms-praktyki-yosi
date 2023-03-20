using Microsoft.AspNetCore.Identity;
using System.Net;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api
{
    public class MagazinesSeeder
    {
        private readonly MagazinesDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MagazinesSeeder(MagazinesDbContext dbContext, RoleManager<IdentityRole> roleManager = null)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Products.Any())
                {
                    var restaurants = GetProducts();
                    _dbContext.Products.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }


            }

        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
/*
        {
            List<Product> products = new List<Product>() {
             new Product {
           ProductName = "Laptop Lenovo ThinkPad",
           EAN = "471385936",
           Price = 1499.99,
           Quantity = 435
       },
       new Product {
           ProductName = "Smartphone Samsung Galaxy",
           EAN = "231487969",
           Price = 799.99,
           Quantity = 744
       },
       new Product {
           ProductName = "Monitor LG UltraFine",
           EAN = "547918379",
           Price = 1299.00,
           Quantity = 921
       },
       new Product {
           ProductName = "Tablet Apple iPad",
           EAN = "728149239",
           Price = 599.99,
           Quantity = 236
       },
       new Product {
         ProductName = "Głośnik Bluetooth JBL",
           EAN = "889452367",
           Price = 249.99,
           Quantity = 534
       },
       new Product {
         ProductName = "Klawiatura Mechaniczna Corsair",
           EAN = "124576892",
           Price = 199.99,
           Quantity = 678
       },
       new Product {
         ProductName = "Mysz Bezprzewodowa Logitech",
           EAN = "436789564",
           Price = 89.99,
           Quantity = 784
       },
       new Product {
         ProductName = "Karta Graficzna Nvidia GeForce",
           EAN = "237894610",
           Price = 699.99,
           Quantity = 319
       },
       new Product {
         ProductName = "Router WiFi TP-Link",
           EAN = "901238474",
           Price = 119.99,
           Quantity = 873
       },
       new Product {
         ProductName = "Słuchawki Bezprzewodowe Sony",
           EAN = "568274901",
           Price = 299.99,
           Quantity = 167
       },
       new Product {
         ProductName = "Kamera internetowa Logitech",
           EAN = "471385937",
           Price = 99.99,
           Quantity = 265
       },

       new Product {
         ProductName = "Klawiatura i mysz bezprzewodowa Microsoft",
           EAN = "231487970",
           Price = 69.99,
           Quantity = 879
       }

       ,
       new Product {
         ProductName = "Smartwatch Samsung Galaxy",
           EAN = "547918380",
           Price = 329.99,
           Quantity = 467
       }

       ,
       new Product {
         ProductName = "Słuchawki Beats by Dr. Dre",
           EAN = "728149240",
           Price = 199.99,
           Quantity = 534
       }

       ,
       new Product {
         ProductName = "Laptop Dell Inspiron",
           EAN = "889452368",
           Price = 849.99,
           Quantity = 342
       }

       ,
       new Product {
         ProductName = "Konsola do gier Sony PlayStation",
           EAN = "124576893",
           Price = 499.99,
           Quantity = 721
       }

       ,
       new Product {
         ProductName = "Oprogramowanie Microsoft Office",
           EAN = "436789565",
           Price = 149.99,
           Quantity = 925
       }

       ,
       new Product {
         ProductName = "Telewizor LG OLED",
           EAN = "237894611",
           Price = 2499.99,
           Quantity = 123
       }

       ,
       new Product {
         ProductName = "Drukarka HP OfficeJet",
           EAN = "901238475",
           Price = 149.99,
           Quantity = 654
       }

       ,
       new Product {
         ProductName = "Głośniki Logitech Surround Sound",
           EAN = "568274902",
           Price = 199.99,
           Quantity = 462
       }

       ,
       new Product {
         ProductName = "Smartfon Apple iPhone",
           EAN = "456321789",
           Price = 1099.99,
           Quantity = 217
       }

       ,
       new Product {
         ProductName = "Karta dźwiękowa Asus Xonar",
           EAN = "786543290",
           Price = 129.99,
           Quantity = 823
       }

       ,
       new Product {
         ProductName = "Mysz gamingowa Razer DeathAdder",
           EAN = "127894356",
           Price = 79.99,
           Quantity = 665
       }

       ,
       new Product {
         ProductName = "Router WiFi Netgear",
           EAN = "674832190",
           Price = 199.99,
           Quantity = 439
       }

       ,
       new Product {
         ProductName = "Tablet Samsung Galaxy Tab",
           EAN = "972341658",
           Price = 449.99,
           Quantity = 567
       }

       ,
       new Product {
         ProductName = "Klawiatura Logitech Illuminated",
           EAN = "137854209",
           Price = 79.99,
           Quantity = 745
       },
       new Product {
         ProductName = "Monitor Dell UltraSharp",
           EAN = "732145987",
           Price = 699.99,
           Quantity = 321
       }

       ,
       new Product {
         ProductName = "Karta graficzna Nvidia GeForce",
           EAN = "876549013",
           Price = 399.99,
           Quantity = 598
       }

       ,
       new Product {
         ProductName = "Słuchawki Sony WH-1000XM4",
           EAN = "290437561",
           Price = 349.99,
           Quantity = 431
       }

       ,
       new Product {
         ProductName = "Oprogramowanie Adobe Creative Cloud",
           EAN = "658943120",
           Price = 599.99,
           Quantity = 172
       }
    };
*/
            return products;
        }

        public async Task<bool> SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Moderator))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            return true;
        }
        
    }
}
