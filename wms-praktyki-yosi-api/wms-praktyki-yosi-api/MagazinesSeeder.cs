using Microsoft.AspNetCore.Identity;
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

                if (!_dbContext.Magazines.Any())
                {
                    var magazine = new Magazine()
                    {
                        Name = "AlfaBeta",
                        Address = "ulica Woronicza 7A, Kraków",
                        Dimentions = "5x10"
                    };
                    _dbContext.Add(magazine);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Shelves.Any())
                {
                    var regalNames = new List<string>() { "A", "B", "C", "D", "E" };
                    var shelves = new List<Shelf>();
                    foreach (string name in regalNames)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            for (int j = 1; j <= 2; j++)
                            {
                                Shelf shelf = new Shelf();
                                shelf.MagazineId = 1;
                                shelf.Position = $"{name}{i}/{j}";
                                shelves.Add(shelf);
                            }
                        }
                    }
                    _dbContext.Shelves.AddRange(shelves);
                    _dbContext.SaveChanges();



                }
                if (!_dbContext.ProductLocations.Any())
                {
                    List<ProductLocations> productLocationsList = new List<ProductLocations>();

                    // Generujemy 40 obiektów ProductLocations z losowymi wartościami
                    Random random = new Random();
                    for (int i = 1; i <= 100; i++)
                    {
                        ProductLocations productLocations = new ProductLocations();
                        productLocations.ProductId = random.Next(1, 41);
                        productLocations.ShelfId = random.Next(1, 101);
                        productLocations.Quantity = random.Next(1, 50);
                        productLocationsList.Add(productLocations);
                    }
                    _dbContext.ProductLocations.AddRange(productLocationsList);
                    _dbContext.SaveChanges();
                }
         

            }

        }

        private List<Product> GetProducts()
        {
            List<Product> _products = new List<Product>
            {
                new Product
                {
                    ProductName = "Microsoft Surface Pro 7",
                    EAN = "889842512167",
                    Price = 7899.99
                },
                new Product
                {
                    ProductName = "Apple MacBook Pro 16",
                    EAN = "190199096057",
                    Price = 17999.99
                },
                new Product
                {
                    ProductName = "Dell XPS 13",
                    EAN = "884116362545",
                    Price = 7199.99
                },
                new Product
                {
                    ProductName = "Asus ROG Strix Scar 17",
                    EAN = "192876861434",
                    Price = 10999.99
                },
                new Product
                {
                    ProductName = "Razer Blade 15",
                    EAN = "811659034008",
                    Price = 9999.99
                },
                    new Product
                {
                    ProductName = "HP Spectre x360",
                    EAN = "193424024798",
                    Price = 8999.99
                },
                new Product
                {
                    ProductName = "Lenovo Legion 5",
                    EAN = "195042640108",
                    Price = 8499.99
                },
                new Product
                {
                    ProductName = "Samsung Galaxy Book Pro",
                    EAN = "887276540087",
                    Price = 7999.99
                },
                new Product
                {
                    ProductName = "LG Gram 17",
                    EAN = "195174006302",
                    Price = 8499.99
                },
                new Product
                {
                    ProductName = "Acer Nitro 5",
                    EAN = "193199948864",
                    Price = 7499.99
                },
                new Product
                {
                    ProductName = "Apple iPad Pro",
                    EAN = "190198859818",
                    Price = 5499.99
                },
                new Product
                {
                    ProductName = "Microsoft Surface Laptop 4",
                    EAN = "889842682558",
                    Price = 5999.99
                },
                new Product
                {
                    ProductName = "Logitech G502 HERO",
                    EAN = "097855146338",
                    Price = 329.99
                },
                new Product
                {
                    ProductName = "Bose QuietComfort 35 II",
                    EAN = "178177700250",
                    Price = 1699.99
                },
                new Product
                {
                    ProductName = "Samsung 860 EVO SSD",
                    EAN = "887276246206",
                    Price = 699.99
                },
                new Product
                {
                    ProductName = "Seagate Backup Plus Slim",
                    EAN = "763649128389",
                    Price = 449.99
                },
                new Product
                {
                    ProductName = "Apple AirPods Pro",
                    EAN = "190199246850",
                    Price = 1299.99
                },
                new Product
                {
                    ProductName = "HyperX Cloud II",
                    EAN = "740617235692",
                    Price = 599.99
                },
                new Product
                {
                    ProductName = "Logitech C920s HD Pro Webcam",
                    EAN = "097855143244",
                    Price = 549.99
                },
                new Product
                {
                    ProductName = "Samsung 4K Smart TV",
                    EAN = "765983427129",
                    Price = 5499.99
                },
                new Product
                {
                    ProductName = "Sony WH-1000XM4",
                    EAN = "741776489932",
                    Price = 1599.99
                },
                new Product
                {
                    ProductName = "Bose Soundbar 700",
                    EAN = "178177458392",
                    Price = 2999.99
                },
                new Product
                {
                    ProductName = "Corsair K95 RGB Platinum XT",
                    EAN = "843591084719",
                    Price = 1299.99
                },
                new Product
                {
                    ProductName = "Philips Hue Starter Kit",
                    EAN = "784984293516",
                    Price = 799.99
                },
                new Product
                {
                    ProductName = "Dell Ultrasharp U2720Q",
                    EAN = "884116366407",
                    Price = 2799.99
                },
                new Product
                {
                    ProductName = "Sennheiser HD 660 S",
                    EAN = "615104312129",
                    Price = 2299.99
                },
                new Product
                {
                    ProductName = "Samsung Galaxy Buds Pro",
                    EAN = "8806092128374",
                    Price = 799.99
                },
                new Product
                {
                    ProductName = "Logitech G Pro X Mechanical Gaming Keyboard",
                    EAN = "5099206086209",
                    Price = 799.99
                },
                new Product
                {
                    ProductName = "Razer DeathAdder V2 Gaming Mouse",
                    EAN = "811659032788",
                    Price = 399.99
                },
                new Product
                {
                    ProductName = "Samsung Odyssey G7 Gaming Monitor",
                    EAN = "8806090428569",
                    Price = 4599.99
                },
                new Product
                {
                    ProductName = "LG UltraFine 4K Display",
                    EAN = "8806098220782",
                    Price = 3399.99
                },
                new Product
                {
                    ProductName = "Jabra Elite 85t True Wireless Earbuds",
                    EAN = "5707055060858",
                    Price = 899.99
                },
                new Product
                {
                    ProductName = "LG UltraWide 34-inch Gaming Monitor",
                    EAN = "719192617107",
                    Price = 2699.99
                },
                new Product
                {
                    ProductName = "Logitech MX Master 3 Wireless Mouse",
                    EAN = "097855148267",
                    Price = 499.99
                },
                new Product
                {
                    ProductName = "Razer BlackWidow Elite Mechanical Gaming Keyboard",
                    EAN = "811659030954",
                    Price = 1099.99
                },
                new Product
                {
                    ProductName = "Sonos Beam Soundbar",
                    EAN = "878269009879",
                    Price = 1999.99
                },
                new Product
                {
                    ProductName = "Canon EOS Rebel T7 DSLR Camera",
                    EAN = "013803301983",
                    Price = 3499.99
                },
                new Product
                {
                    ProductName = "Samsung Galaxy Tab S7+",
                    EAN = "887276438834",
                    Price = 2799.99
                },
                new Product
                {
                    ProductName = "Microsoft Surface Pen",
                    EAN = "889842202700",
                    Price = 499.99
                },
                new Product
                {
                    ProductName = "Seagate Backup Plus Slim 2TB Portable Hard Drive",
                    EAN = "763649129241",
                    Price = 529.45
                }
                // and so on...
            };
            return _products;
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