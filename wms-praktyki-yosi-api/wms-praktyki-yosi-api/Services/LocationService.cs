using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
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
                ?? throw new NotFoundException("151");

            var shelf = _context
                .Shelves
                .Include(s => s.Locations)
                .FirstOrDefault(s => s.Position == location.Position)
                ?? throw new NotFoundException("150"); //czy półka istnieje 

            var quantityOnShelf = shelf
                .Locations
                .Sum(r => r.Quantity);

            if (quantityOnShelf + location.Quantity > shelf.MaxLoad)
                throw new BadRequestException("180");

            var mappedProd = _mapper.Map<ProductLocations>(location);
            mappedProd.ShelfId = shelf.Id;
            _context.ProductLocations.Add(mappedProd);
            _context.SaveChanges();

            return product.Id;
        }

        public IEnumerable<ProductLocationDto> GetAllLocations()
        {
            var dtos = _context
                .ProductLocations
                .Include(r => r.Shelf)
                .Select(p => new ProductLocationDto()
                    {
                        ProductId = p.ProductId,
                        Position = p.Shelf.Position,
                        Quantity = p.Quantity
                    });

            return dtos;
        }
        public ReturnProductLocationDto GetLocationById(int id)
        {
            var loc = _context
                .ProductLocations
                .Include(l => l.Shelf)
                .FirstOrDefault(r => r.Id == id)
                ?? throw new NotFoundException("152");


            var res = _mapper.Map<ReturnProductLocationDto>(loc);

            return res;
        }

        public void UpdateLocation(int id, EditProductLocationDto location)
        {
            var loc = _context
                .ProductLocations
                .FirstOrDefault(r => r.Id == id)
                ?? throw new NotFoundException("152");

            
            var shelf = _context
                .Shelves
                .Include(s => s.Locations)
                .FirstOrDefault(s => s.Position == location.Position && s.MagazineId == location.MagazineId)
                ?? throw new NotFoundException("150");
            
            var quantityOnShelf = shelf
                .Locations
                .Where(r => r.Id != id)
                .Sum(r => r.Quantity);

            if (quantityOnShelf + location.Quantity > shelf.MaxLoad)
                throw new BadRequestException("180");

            loc.ShelfId = shelf.Id;
            loc.Quantity = location.Quantity;

            _context.SaveChanges();
        }
        public void DeleteLocation(int id)
        {
            var loc = _context
                .ProductLocations
                .FirstOrDefault(r => r.Id == id) 
                ?? throw new NotFoundException("152");

            _context.ProductLocations.Remove(loc);
            _context.SaveChanges();
        }
    }
}
