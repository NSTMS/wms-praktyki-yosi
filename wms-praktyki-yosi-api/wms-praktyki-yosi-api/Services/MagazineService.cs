using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api.Services
{
    public class MagazineService : IMagazineService
    {
        private readonly MagazinesDbContext _context;
        private readonly IMapper _mapper;

        public MagazineService(MagazinesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Magazine> GetAll()
        {
            var res = _context
                .Magazines
                .Include(m => m.Shelves)
                .ToList();
            return res;
        }
        public MagazineDto GetById(int id)
        {
            var magazine = _context
                .Magazines
                .Include(s => s.Shelves)
                .FirstOrDefault(x => x.Id == id);
            if (magazine == null)
                return null;
            var res = _mapper.Map<MagazineDto>(magazine);
            return res;
        }
        public List<ProductLocationDto> GetLocationsInMagazine(int id)
        {
            var magazines = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(r => r.Shelf.MagazineId == id)
                .ToList();
            var res = _mapper.Map<List<ProductLocationDto>>(magazines);
            return res;
        }
        public ProductDto GetProductInMagazine(int id, int productId)
        {
            var locations = _context
              .ProductLocations
              .Include(s => s.Shelf)
              .Include(s => s.Product)
              .Where(r => r.Shelf.MagazineId == id);
            if (locations == null)
                return null;

            var firstProdLocation =
                locations
                .FirstOrDefault(p => p.ProductId == id);
            if (firstProdLocation == null)
                return null;
            var product = firstProdLocation.Product;

            var res = new ProductDto()
            {
                ProductName = product.ProductName,
                EAN = product.EAN,
                Price = product.Price,
                Locations = _mapper.Map<List<ReturnProductLocationDto>>(locations),
                Quantity = locations.Sum(l => l.Quantity)
            };
            return res;
        }


        public List<ProductDto> GetProductsInMagazine(int id)
        {
            var prodIds = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Include(p => p.Product)
                .Where(l => l.Shelf.MagazineId == id)
                .Select(l => l.ProductId);

            if (prodIds == null)
                return null;

            var products = _context
                .Products
                .Include(r => r.Locations)
                .Where(p => prodIds.Contains(p.Id));

            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                EAN = p.EAN,
                Price = p.Price,
                Quantity = p.Locations.Sum(l => l.Quantity)
            }).ToList();
            return dtos;
        }
        public List<ReturnProductLocationDto> GetLocationsOfProduct(int id, int productId)
        {
            var magzaine = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(p => p.Shelf.MagazineId == id);
            if (magzaine == null)
                return null;

            var locations = magzaine
                .Where(p => p.ProductId == productId).ToList();
            if (locations == null)
                return null;
            var res = _mapper.Map<List<ReturnProductLocationDto>>(locations);
            return res;
        }
        public int AddMagzine(MagazineDto dto)
        {
            try
            {
                var res = _mapper.Map<Magazine>(dto);
                var shelvesList = new List<Shelf>();

                var regalNames = new List<string>() { "A", "B", "C", "D", "E" ,"F","G","H","I","J","K","L","M","N","O","P","R","S","T","U","W","Y","Z"};
                if (dto.Dimentions == null)
                    throw new NullReferenceException("Dimentions were invalid");
                var dimentions = dto.Dimentions.Split("x");


                foreach (string name in regalNames)
                {
                    for (int i = 1; i <= Convert.ToInt32(dimentions[0]); i++)
                    {
                        for (int j = 1; j <= Convert.ToInt32(dimentions[1]); j++)
                        {
                            Shelf shelf = new Shelf();
                            shelf.MagazineId = res.Id;
                            shelf.Position = $"{name}{i}/{j}";
                            shelvesList.Add(shelf);
                        }
                    }
                }
                res.Shelves = new List<Shelf>();
                res.Shelves.AddRange(shelvesList);

                _context.Shelves.AddRange(shelvesList);
                _context.Magazines.Add(res);
                _context.SaveChanges();
                return res.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool UpdateMagazine(int id, MagazineDto dto)
        {
            var magazine = _context.Magazines.FirstOrDefault(m => m.Id == id);
            if (magazine == null)
                return false;

            magazine.Address = dto.Address;
            magazine.Name = dto.Name;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMagazine(int id)
        {
            var magazine = _context.Magazines.FirstOrDefault(m => m.Id == id);
            if (magazine == null)
                return false;

            _context.Magazines.Remove(magazine);
            _context.SaveChanges();
            return true;
        }


    }
}
