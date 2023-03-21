using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Services
{
    public class ProductService : IProductService
    {
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
            _context.SaveChanges();

            return true;
        }
        public IEnumerable<ProductDto> GetAll()
        {
            var seeder = new MagazinesSeeder(_context);
            seeder.Seed();
            var products = _context.Products.Include(p => p.Locations).ToList();
            var dtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                EAN = p.EAN,
                Price = p.Price,
                Quantity = p.Locations.Sum(l => l.Quantity)
            });
            return dtos;
        }

        public ProductDto GetById(int id)
        {
            var product = _context.Products
                .Include(p => p.Locations)
                .FirstOrDefault(r => r.Id == id);

            if (product == null)
                return null;

            var loc = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(s => s.ProductId == id).ToList();



            var res = new ProductDto()
            {
                ProductName = product.ProductName,
                EAN = product.EAN,
                Price = product.Price,
                Locations = _mapper.Map<List<ReturnProductLocationDto>>(loc),
                Quantity = loc.Sum(l => l.Quantity)
            };

            return res;


        }
    }
}
