using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Services
{
    public class ProductService : IProductService
    {
        private readonly MagazinesDbContext _context;
        private readonly IMapper _mapper;

        private readonly Dictionary<string, Expression<Func<ProductDto, object>>> _orderByColumnSelector = new()
        {
            {nameof(ProductDto.ProductName), p =>  p.ProductName},
            {nameof(ProductDto.EAN), p => p.EAN },
            {nameof(ProductDto.Price), p => p.Price},
            {nameof(ProductDto.Quantity), p => p.Quantity},
        };



        public ProductService(MagazinesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAll(GetRequestQuery query)
        {
            var seeder = new MagazinesSeeder(_context);
            seeder.Seed();

            var productDtos = _context.Products
                .Include(p => p.Locations)
                .Where(p => (query.SearchTerm == null) || p.ProductName.ToLower().Contains(query.SearchTerm.ToLower())
                                                       || p.EAN.ToLower().Contains(query.SearchTerm.ToLower()))
                .Select(p => new ProductDto
                 {
                     Id = p.Id,
                     ProductName = p.ProductName,
                     EAN = p.EAN,
                     Price = p.Price,
                     Quantity = p.Locations.Sum(l => l.Quantity)
                 });
            if (query.OrderBy != null)
            {
                try
                {
                    var selectedColum = _orderByColumnSelector[query.OrderBy];
                    productDtos = (query.Descending)
                    ? productDtos.OrderByDescending(selectedColum)
                    : productDtos.OrderBy(selectedColum);
                }
                catch (KeyNotFoundException)
                {
                    throw new BadRequestException("U bad");
                }

                
            }

            return productDtos;
        }

        public ProductDto GetById(int id)
        {
            var product = _context.Products
                .Include(p => p.Locations)
                .FirstOrDefault(r => r.Id == id)
                ?? throw new NotFoundException("151");

            var loc = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(s => s.ProductId == id);

            var res = new ProductDto()
            {
                ProductName = product.ProductName,
                EAN = product.EAN,
                Price = product.Price,
                Locations = _mapper.Map<List<ReturnProductLocationDto>>(loc.ToList()),
                Quantity = loc.Sum(l => l.Quantity)
            };

            return res;
        }

        public int AddNewProduct(ProductDto dto) {
            var product = _mapper.Map<Product>( dto );
            _context.Products
                .Add( product );
            _context.SaveChanges();
            return product.Id;
            
        }
        
        public void UpdateProduct(int id, ProductDto dto) {
            var prod = _context.Products
                .FirstOrDefault(r => r.Id == id)
                ?? throw new NotFoundException("151");

            prod.ProductName = dto.ProductName;
            prod.EAN = dto.EAN;
            prod.Price = dto.Price;
            _context.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
            var prod = _context.Products
                .FirstOrDefault(r => r.Id == id)
                ?? throw new NotFoundException("151");

            _context
                 .Products
                 .Remove(prod);

            _context.SaveChanges();
        }


    }
}
