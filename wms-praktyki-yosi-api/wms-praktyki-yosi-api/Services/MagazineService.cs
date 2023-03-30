using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Services.Static;

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

        public IEnumerable<ReturnMagazineDto> GetAll(GetRequestQuery query)
        {
            var magzines = _context
                .Magazines
                .Where(m => (query.SearchTerm == null) || m.Name.ToLower().Contains(query.SearchTerm.ToLower())
                                                       || m.Address.ToLower().Contains(query.SearchTerm.ToLower()))
                .ToList();


            var magazineDtos = new List<ReturnMagazineDto>();
            foreach(var magazine in magzines)
            {
                var magazineDto = ConvertMagazineToDto(magazine);
                magazineDtos.Add(magazineDto);
            }
            var magazineDtosQuery = magazineDtos.AsQueryable();
            if (query.OrderBy != null)
            {
                try
                {
                    var selectedColum = OrderByColumnSelectors.Magazines[query.OrderBy.ToLower()];
                    magazineDtosQuery = (query.Descending)
                    ? magazineDtosQuery.OrderByDescending(selectedColum)
                    : magazineDtosQuery.OrderBy(selectedColum);
                }
                catch (KeyNotFoundException)
                {
                    throw new BadRequestException("U bad");
                }
            }

            return magazineDtosQuery;
        }
        public ReturnMagazineDto GetById(int id)
        {
            var magazine = GetMagazineById(id);

            ReturnMagazineDto magazineDto = ConvertMagazineToDto(magazine);

            return magazineDto;
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
            var magazine = GetMagazineWithShelvesById(id);

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
        public List<ProductDto> GetProductsInMagazine(int id, GetRequestQuery query)
        {
            var prodIds = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Include(p => p.Product)
                .Where(l => l.Shelf.MagazineId == id)
                .Select(l => l.ProductId)
                ?? throw new NotFoundException("152");

            var products = _context
                .Products
                .Include(r => r.Locations)
                .Where(p => prodIds.Contains(p.Id)
                    && ((query.SearchTerm == null) || p.ProductName.ToLower().Contains(query.SearchTerm.ToLower())
                                                   || p.EAN.ToLower().Contains(query.SearchTerm.ToLower())));
 

            var dtos = products
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
                    var selectedColum = OrderByColumnSelectors.Products[query.OrderBy.ToLower()];
                    dtos = (query.Descending)
                    ? dtos.OrderByDescending(selectedColum)
                    : dtos.OrderBy(selectedColum);
                }
                catch (KeyNotFoundException)
                {
                    throw new BadRequestException("No column of that name");
                }
            }

            return dtos.ToList();
        }
        public List<ReturnProductLocationDto> GetLocationsOfProduct(int id, int productId)
        {
            var LocationsInMagzaine = _context
                .ProductLocations
                .Include(s => s.Shelf)
                .Where(p => p.Shelf.MagazineId == id);

            if (!LocationsInMagzaine.Any())
            {
                throw new NotFoundException("153");
            }

            var locations = LocationsInMagzaine
                .Where(p => p.ProductId == productId)
                .ToList();
            if (!locations.Any())
                throw new NotFoundException("152");

            var res = _mapper.Map<List<ReturnProductLocationDto>>(locations);
            return res;
        }
        public int AddMagzine(MagazineDto dto)
        {
            var magazine = _mapper.Map<Magazine>(dto);

            if (dto.Dimentions == null)
                throw new BadRequestException("117");

            
            _context.Magazines.Add(magazine);
            ConcurencyResolver.SafeSave(_context);
            var magazineId = magazine.Id;
            List<Shelf>? shelfList;

            try 
            { 
                shelfList = GetShelvesList(dto, magazineId);
            }
            catch
            {
                _context.Magazines.Remove(magazine);
                ConcurencyResolver.SafeSave(_context);
                throw new BadRequestException("117");
            }

            
            _context.Shelves.AddRange(shelfList);
            ConcurencyResolver.SafeSave(_context);
            return magazine.Id;

        }
        public void UpdateMagazine(int id, EditMagazineDto dto)
        {
            var magazine = GetMagazineById(id);


            magazine.Address = dto.Address;
            magazine.Name = dto.Name;
            ConcurencyResolver.SafeSave(_context);
        }
        public void DeleteMagazine(int id)
        {
            var magazine = GetMagazineById(id);

            magazine.Deleted = true;
            ConcurencyResolver.SafeSave(_context);
        }


        private List<string> GetRegalNames(int count)
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
        private List<string> GetAllPositions(int x, int y, int z)
        {
            var positions = new List<string>();
            var regalNames = GetRegalNames(Convert.ToInt32(x));

            foreach (string name in regalNames)
            {
                for (int i = 1; i <= Convert.ToInt32(y); i++)
                {
                    for (int j = 1; j <= Convert.ToInt32(z); j++)
                    {
                        positions.Add($"{name}{i}/{j}");
                    }
                }
            }
            return positions;
        }
        private Magazine GetMagazineById(int id)
        {
            return _context.Magazines
                .FirstOrDefault(m => m.Id == id && !m.Deleted)
                ?? throw new NotFoundException("153");
        }
        private Magazine GetMagazineWithShelvesById(int id)
        {
            return _context.Magazines
                .Include(m => m.Shelves)
                .FirstOrDefault(m => m.Id == id && !m.Deleted)
                ?? throw new NotFoundException("153");
        }
        private List<Shelf> GetShelvesList(MagazineDto dto, int magazineId)
        {
            var shelvesList = new List<Shelf>();

            var dimentions = dto.Dimentions.Split("x");
            var shelvesPR = dto.ShelvesPerRow;

            var positions = GetAllPositions(
                    Convert.ToInt32(dimentions[0]),
                    Convert.ToInt32(dimentions[1]),
                    Convert.ToInt32(shelvesPR)
                );

            foreach (var pos in positions)
            {
                Shelf shelf = new()
                {
                    MagazineId = magazineId,
                    Position = pos,
                    MaxLoad = dto.MaxShelfQuantity
                };
                shelvesList.Add(shelf);
            }
            return shelvesList;
        }
        private ReturnMagazineDto ConvertMagazineToDto(Magazine magazine)
        {
            var shelvesInMagzine = _context
                                .Shelves
                                .Where(s => s.MagazineId == magazine.Id);

            var maxShelfLoad = shelvesInMagzine.Min(s => s.MaxLoad);
            var numberOfShefls = shelvesInMagzine.Count();
            var totalCapacity = shelvesInMagzine.Sum(s => s.MaxLoad);
            shelvesInMagzine = null;
            var totalQuantity = _context
                .ProductLocations
                .Include(l => l.Shelf)
                .Where(l => l.Shelf.MagazineId == magazine.Id)
                .Sum(l => l.Quantity);

            var magazineDto = new ReturnMagazineDto
            {
                Id = magazine.Id,
                Name = magazine.Name,
                Address = magazine.Address,
                Dimentions = magazine.Dimentions,
                ShelvesPerRow = magazine.ShelvesPerRow,
                MaxShelfLoad = maxShelfLoad,
                ShelfNumber = numberOfShefls,
                TotalCapacity = totalCapacity,
                TotalQuantity = totalQuantity,
                FreeSpace = totalCapacity - totalQuantity
            };
            return magazineDto;
        }

    }
}
