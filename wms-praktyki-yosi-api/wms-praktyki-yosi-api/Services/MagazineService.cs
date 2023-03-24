using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
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
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException("153");
            var res = _mapper.Map<MagazineDto>(magazine);
            return res;
        }
        public List<ProductLocationDto> GetLocationsInMagazine(int id)
        {
            var locations = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(r => r.Shelf.MagazineId == id)
                .ToList();
            var res = _mapper.Map<List<ProductLocationDto>>(locations);
            return res;
        }
        public ProductDto GetProductInMagazine(int id, int productId)
        {

            var magazine = _context
                .Magazines
                .Include(s => s.Shelves)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException("153");

            var locations = _context
              .ProductLocations
              .Include(s => s.Shelf)
              .Include(s => s.Product)
              .Where(r => r.Shelf.MagazineId == id)
              .Where(r => r.ProductId == productId)
              ?? throw new NotFoundException("152");

            var firstProdLocation =
                locations
                .FirstOrDefault()
                ?? throw new NotFoundException("152");
                 
            var product = firstProdLocation.Product;

            var res = new ProductDto()
            {
                Id = productId,
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
                throw new NotFoundException("no locations found"); // TODO: Add Error Codes

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
                throw new NotFoundException("153");

            var locations = magzaine
                .Where(p => p.ProductId == productId).ToList();
            if (locations == null)
                throw new NotFoundException("152");
            var res = _mapper.Map<List<ReturnProductLocationDto>>(locations);
            return res;
        }
        public int AddMagzine(MagazineDto dto)
        {
            var magazine = _mapper.Map<Magazine>(dto);
            var shelvesList = new List<Shelf>();

            if (dto.Dimentions == null)
                throw new BadRequestException("117");

            var dimentions = dto.Dimentions.Split("x");
            var shelvesPR = dto.ShelvesPerRow;
            _context.Magazines.Add(magazine);
            _context.SaveChanges();
            try
            {
                
                var regalNames = GetRegalNames(Convert.ToInt32(dimentions[0]));
            
                foreach (string name in regalNames)
                {
                    for (int i = 1; i <= Convert.ToInt32(dimentions[1]); i++)
                    {
                        for (int j = 1; j <= Convert.ToInt32(shelvesPR); j++)
                        {
                            Shelf shelf = new Shelf();
                            shelf.MagazineId = magazine.Id;
                            shelf.Position = $"{name}{i}/{j}";
                            shelvesList.Add(shelf);
                        }
                    }
                }
                
            }
            catch
            {
                _context.Magazines.Remove(magazine);
                _context.SaveChanges();
                throw new BadRequestException("117");
            }

            
            _context.Shelves.AddRange(shelvesList);
            _context.SaveChanges();
            return magazine.Id;

        }
        public void UpdateMagazine(int id, MagazineDto dto)
        {
            var magazine = _context.Magazines
                .FirstOrDefault(m => m.Id == id)
                ?? throw new NotFoundException("1");


            magazine.Address = dto.Address;
            magazine.Name = dto.Name;
            _context.SaveChanges();
        }

        public void DeleteMagazine(int id)
        {
            var magazine = _context.Magazines
                .FirstOrDefault(m => m.Id == id)
                ?? throw new NotFoundException("1");

            _context.Magazines.Remove(magazine);
            _context.SaveChanges();
        }

        public List<string> GetRegalNames(int count)
        {
            var num = Convert.ToInt32(count);
            var NUM = num;
            var smallest = num % 26;
            var regalNames = new List<string>();

            while (num > 0)
            {
                int digit = num % 26;
                num /= 26;
                if (num > 0) continue;

                for (int i = 0; i < NUM && i < 26; i++)
                {
                    regalNames.Add((char)(i + 'A') + "");
                }
                if (NUM > 26)
                {
                    for (int i = 0; i < digit - 1; i++)
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            regalNames.Add((char)(i + 'A') + "" + (char)(j + 'A'));
                        }
                    }

                    for (int j = 0; j < 26 && j < smallest; j++)
                    {
                        regalNames.Add((char)(digit - 1 + 'A') + "" + (char)(j + 'A'));
                    }
                }
                

            }
            return regalNames;
        }

    }
}
