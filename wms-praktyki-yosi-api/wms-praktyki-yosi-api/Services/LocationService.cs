using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
namespace wms_praktyki_yosi_api.Services
{

    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private MagazinesDbContext _context;

        public LocationService(IMapper mapper, MagazinesDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public int AddProductToLocation(ProductLocationDto location)
        {
            var product = _context
                .Products
                .FirstOrDefault(p => p.Id == location.ProductId) 
                ?? throw new BadRequestException("151");
            var shelf = _context
                .Shelves
                .FirstOrDefault(s => s.Position == location.Position)
                ?? throw new BadRequestException("150"); //czy półka istnieje 

            var prodLoc = _context
                .ProductLocations
                .Include(l => l.Shelf)
                .FirstOrDefault(l => location.Position ==  l.Shelf.Position); //czy prod znajduje sie na półce


            if (prodLoc != null)
            {
                prodLoc.Quantity += location.Quantity;
                _context.SaveChanges();
            }
            else
            {
                var mappedProd = _mapper.Map<ProductLocations>(location);
                mappedProd.ShelfId = shelf.Id;
                _context.ProductLocations.Add(mappedProd);
                _context.SaveChanges();

            }
            return product.Id;
        }

        public IEnumerable<ProductLocationDto> GetAllLocations()
        {
            var locations = _context.ProductLocations.Include(r => r.Shelf).ToList();
            var dtos = locations.Select(p => new ProductLocationDto
            {
                ProductId = p.ProductId,
                Position = p.Shelf.Position,
                Quantity = p.Quantity
            });
            if (dtos == null) 
                return null;
            return dtos;
        }
        public ReturnProductLocationDto GetLocationById(int id)
        {
            var loc = _context
                .ProductLocations
                .Include(l => l.Shelf)
                .FirstOrDefault(r => r.Id == id);

            if (loc == null) 
                return null;

            var res = _mapper.Map<ReturnProductLocationDto>(loc);

            return res;
        }

        public bool UpdateLocation(int id, ProductLocationDto location)
        {
            var loc = _context
                .ProductLocations
                .FirstOrDefault(r => r.Id == id);

            if (loc == null)
                return false;
            
            var shelf = _context.Shelves.FirstOrDefault(s => s.Position == location.Position);
            if (shelf == null)
                return false;

            loc.ShelfId= shelf.Id;
            loc.Quantity = location.Quantity;

            _context.SaveChanges();
            return true;
        }
        public bool DeleteLocation(int id)
        {
            var loc = _context.ProductLocations.FirstOrDefault(r => r.Id == id);
            if (loc == null)
                return false;
            _context.ProductLocations.Remove(loc);

            _context.SaveChanges();
            return true;
        }
    }
}
