using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using wms_praktyki_yosi_api.Enitities;
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

        public int AddNewLocation(ProductLocationDto location)
        {
            var loc = _mapper.Map<ProductLocations>(location);
            _context.ProductLocations.Add(loc);
            _context.SaveChanges();
            return loc.Id;
        }

        public IEnumerable<ProductLocationDto> GetAllLocations()
        {
            var locations = _context.ProductLocations.Include(r => r.Id).ToList();
            var dtos = locations.Select(p => new ProductLocationDto
            {
                ProductId = p.ProductId,
                ShelfId = p.ShelfId,
                Quantity = p.Quantity
            });
            if (dtos == null) return null;
            return dtos;
        }
        public ProductLocations GetLocationById(int id)
        {
            var loc = _context.ProductLocations.FirstOrDefault(r => r.Id == id);
            if (loc == null) return null;
            return loc;
        }

        public bool UpdateLocation(int id, ProductLocationDto location)
        {
            var loc = _context.ProductLocations.FirstOrDefault(r => r.Id == id);

            if (loc == null)
            {
                return false;
            }
            loc.ProductId = location.ProductId;
            loc.ShelfId = location.ShelfId;
            loc.Quantity = loc.Quantity;

            _context.SaveChanges();
            return true;
        }
        public bool DeleteLocation(int id)
        {
            var loc = _context.ProductLocations.FirstOrDefault(r => r.Id == id);
            if (loc == null)
            {
                return false;
            }
            _context.ProductLocations.Remove(loc);

            _context.SaveChanges();
            return true;
        }
    }
}
