using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models.DocumentModels;

namespace wms_praktyki_yosi_api.Services
{
    public interface IDocumentService
    {
        List<DocumentDto> GetAllDocuments();
        string AddDocument(AddDocumentDto dto);
        void AddItemToDocument(string id, AddDocumentItemDto item);
        void DeleteDocument(string id);
        void DeleteDocumentItem(string documentId, int productId);
        void MarkDocumentAsFinished(string id, bool finished);
        void UpdateItemInDocument(string documentId, int productId, EditDocumentItemDto item);
        void VisitLocation(string documentId, DocumentVisitLocationDto location);
        DetailedDocumentDto GetDocumentDetails(string id);
    }

    public class DocumentService : IDocumentService
    {
        private readonly MagazinesDbContext _context;
        private readonly IMapper _mapper;

        public DocumentService(MagazinesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<DocumentDto> GetAllDocuments()
        {
            var documents = _context
                .Documents
                .Include(d => d.Items)
                .Where(d => !d.Deleted)
                .ToList();

            var documentDtos = _mapper.Map<List<DocumentDto>>(documents);
            return documentDtos;
        }
        public DetailedDocumentDto GetDocumentDetails(string id)
        {
            var document = GetDocumentByGuid(id);
            document.Items = _context
                .DocumentItems
                .Where(i => i.DocumentId == document.Id)
                .Include(i => i.Product)
                .ToList();
            var docuemntDto = _mapper.Map<DetailedDocumentDto>(document);
            return docuemntDto;
        }
        public string AddDocument(AddDocumentDto dto)
        {
            var doc = _mapper.Map<Document>(dto);

            _context.Documents.Add(doc);
            _context.SaveChanges();
            try
            {
                var docItems = GetDocumentItems(doc.Id, dto.ItemList, dto.MagazineId);
                _context.DocumentItems.AddRange(docItems);
                _context.SaveChanges();
        }
            catch
            {
                _context.Documents.Remove(doc);
                _context.SaveChanges();
                throw;
            }

            return doc.Id.ToString();
        }
        public void DeleteDocument(string id)
        {
            var document = GetDocumentByGuid(id);

            document.Deleted = true;
            _context.SaveChanges();
        }
        public void MarkDocumentAsFinished(string id, bool finished)
        {
            var document = GetDocumentByGuid(id);

            document.Finished = finished;
            _context.SaveChanges();
        }
        public void AddItemToDocument (string id, AddDocumentItemDto item)
        {
            var document = GetDocumentWithItemsByGuid(id);

            var productId = GetProductId(item.ProductName);


            var itemsInDocument = document
                .Items
                .Where(i => i.ProductId == productId);


            if (itemsInDocument.Any())
            {

                int newQuantity = 0;
                int itemQuantity = item.Quantity;

                // Srry for this bullshiet code, i dont even balive it works
                var oldQuantity = itemsInDocument.Sum(i => i.Quantityplaned);

                if (itemsInDocument.First().Arriving == item.Arriving)
                {
                    newQuantity = oldQuantity + itemQuantity;
                }
                else if (itemQuantity > oldQuantity)
                {
                    newQuantity = itemQuantity - oldQuantity;
                }
                else if (itemQuantity < oldQuantity)
                {
                    newQuantity = oldQuantity - itemQuantity;
                    item.Arriving = !item.Arriving;
                }
                

                _context.DocumentItems.RemoveRange(itemsInDocument);
                _context.SaveChanges();
                if (newQuantity == 0) return;
                item.Quantity = newQuantity;
            }

            var itemsToAdd = ConvertDocumentItemDtoToItemList(document.Id, document.MagazineId, item);

            _context
                .DocumentItems
                .AddRange(itemsToAdd);
            _context.SaveChanges();
        }
        public void DeleteDocumentItem(string documentId, int productId)
        {
            var document = GetDocumentWithItemsByGuid(documentId);

            var itemsToDelete = document
                .Items
                .Where(i => i.ProductId == productId);
            
            _context.RemoveRange(itemsToDelete);
            _context.SaveChanges();
        }
        public void UpdateItemInDocument(string documentId, int productId, EditDocumentItemDto item)
        {
            var document = GetDocumentWithItemsByGuid(documentId);

            var product = _context
                .Products
                .FirstOrDefault(p => p.Id == productId)
                ?? throw new NotFoundException("151");

            var newItem = new AddDocumentItemDto()
            {
                ProductName = product.ProductName,
                Arriving = item.Arriving,
                Quantity = item.Quantity,
                Tag = item.Tag
            };
            var itemsToDelete = document
                .Items
                .Where(i => i.ProductId == productId);

            _context.RemoveRange(itemsToDelete);
            _context.SaveChanges();

            var itemList = ConvertDocumentItemDtoToItemList(document.Id, document.MagazineId, newItem);

            _context.AddRange(itemList);
            _context.SaveChanges();
        }
        public void VisitLocation(string documentId, DocumentVisitLocationDto location)
        {
            var document = GetDocumentByGuid(documentId);

            var documentItem = _context
                .DocumentItems
                .Include(di => di.Product)
                .FirstOrDefault(di =>
                    di.DocumentId == document.Id &&
                    di.Product.ProductName == location.ProductName &&
                    di.Position == location.Position &&
                    (location.Tag == null || location.Tag == di.Tag)
                ) ?? throw new NotFoundException("156");

            if (documentItem.Arriving)
                PutProductOnShelf(document.MagazineId, location);
            else
                TakeProductFromShelf(document.MagazineId, location);

            documentItem.QuantityDone += location.Quantity;
            _context.SaveChanges();
        }
        
        // ------- Private Functions --------
        private Document GetDocumentByGuid(string guid)
        {
            return _context
                .Documents
                .FirstOrDefault(d => d.Id.ToString() == guid)
                ?? throw new NotFoundException("155");
        }
        private Document GetDocumentWithItemsByGuid(string guid)
        {
            return _context
                .Documents
                .Include(d => d.Items)
                .FirstOrDefault(d => d.Id.ToString() == guid)
                ?? throw new NotFoundException("155");
        }
        private int GetProductId(string productName)
        {
            return (
                    _context
                    .Products
                    .FirstOrDefault(p => p.ProductName == productName)
                    ?? throw new NotFoundException("151")
                ).Id;
        }
        private List<DocumentItem> GetDocumentItems(
            Guid documentId,
            List<AddDocumentItemDto> itemList,
            int magazineId
        )
        {

            var docItems = new List<DocumentItem>();
            foreach (var item in itemList)
            {
                List<DocumentItem> items = ConvertDocumentItemDtoToItemList(documentId, magazineId, item);

                docItems.AddRange(items);
            }

            return docItems;
        }

        private List<DocumentItem> ConvertDocumentItemDtoToItemList(
            Guid documentId,
            int magazineId,
            AddDocumentItemDto item
        )
        {
            var prodId = GetProductId(item.ProductName);
            List<DocumentItem> items;
            if (item.Arriving)
                items = GetDocumentItemListForArravingItem(documentId, item, magazineId, prodId);
            else
                items = GetDocumentItemListForItem(documentId, item, magazineId, prodId);

            return items;
        }

        private List<DocumentItem> GetDocumentItemListForItem(
            Guid documentId,
            AddDocumentItemDto item,
            int magazineId,
            int prodId
        )
        {
            var items = new List<DocumentItem>();

            

            var productLocations = _context
                .ProductLocations
                .Include(x => x.Shelf)
                .Where(x => x.ProductId == prodId && x.Shelf.MagazineId == magazineId)
                .OrderBy(x => x.Quantity);

            var productSum = productLocations.Sum(x => x.Quantity);

            var quantityToTake = item.Quantity;

            if (productSum < quantityToTake)
                throw new BadRequestException("181");

            foreach (var location in productLocations)
            {
                int quantityTaken;
                if (quantityToTake >= location.Quantity)
                {
                    quantityTaken = location.Quantity;
                }
                else
                {
                    quantityTaken = quantityToTake;
                }
                quantityToTake -= quantityTaken;
                items.Add(new DocumentItem()
                {
                    Arriving = false,
                    Tag = location.Tag,
                    Position = location.Shelf.Position,
                    Quantityplaned = quantityTaken,
                    MagzineId = magazineId,
                    DocumentId = documentId,
                    ProductId = prodId,
                });

                if (quantityToTake <= 0)
                    break;
            }

            return items;
        }

        private List<DocumentItem> GetDocumentItemListForArravingItem(
            Guid documentId,
            AddDocumentItemDto item,
            int magazineId,
            int prodId
        )
        {

            var shelves = _context
                .Shelves
                .Include(s => s.Locations)
                .Where(s => s.MagazineId == magazineId)
                .Select(s => new
                {
                    FreeSpace = s.MaxLoad - s.Locations.Sum(l => l.Quantity),
                    s.Position
                })
                .OrderByDescending(s => s.FreeSpace)
                .ToList();

            var freeSpaces = shelves.Sum(s => s.FreeSpace);
            var quatityToAllocate = item.Quantity;

            if (freeSpaces < quatityToAllocate)
                throw new BadRequestException("182");

            var items = new List<DocumentItem>();
            foreach (var shelf in shelves)
            {
                int quantityToPlace;
                if (quatityToAllocate < shelf.FreeSpace)
                    quantityToPlace = quatityToAllocate;
                else
                    quantityToPlace = shelf.FreeSpace;

                quatityToAllocate -= quantityToPlace;

                items.Add(
                     new DocumentItem()
                     {
                         Arriving = true,
                         Tag = item.Tag,
                         Quantityplaned = quantityToPlace,
                         MagzineId = magazineId,
                         DocumentId = documentId,
                         ProductId = prodId,
                         Position = shelf.Position
                     }
                    );
               if (quatityToAllocate <= 0)
                {
                    break;
                }
            };
            return items;
        }

        private void PutProductOnShelf(int magazineId, DocumentVisitLocationDto location)
        {

            var shelf = _context
                    .Shelves
                    .Include(s => s.Locations)
                    .FirstOrDefault(s =>
                        s.Position == location.Position &&
                        s.MagazineId == magazineId
                    ) ?? throw new NotFoundException("150");

            var spaceOnShelf = shelf.MaxLoad - shelf.Locations.Sum(l => l.Quantity);

            if (location.Quantity > spaceOnShelf)
                throw new BadRequestException("180");

            var itemLocation = _context
                .ProductLocations
                .Include(pl => pl.Product)
                .Include(pl => pl.Shelf)
                .FirstOrDefault(pl =>
                    pl.Product.ProductName == location.ProductName &&
                    pl.Shelf.Position == location.Position &&
                    (location.Tag == null || location.Tag == pl.Tag)
                );

            if (itemLocation == null)
            {
                var productLocation = new ProductLocations()
                {
                    ShelfId = shelf.Id,
                    ProductId = GetProductId(location.ProductName),
                    Quantity = location.Quantity,
                    Tag = location.Tag ?? ""
                };
                _context.ProductLocations.Add(productLocation);
                return;
            }

            itemLocation.Quantity += location.Quantity;  
        }

        private void TakeProductFromShelf(int magazineId, DocumentVisitLocationDto location)
        {
            var itemLocation = _context
                .ProductLocations
                .Include(pl => pl.Product)
                .Include(pl => pl.Shelf)
                .FirstOrDefault(pl =>
                    pl.Product.ProductName == location.ProductName &&
                    pl.Shelf.Position == location.Position &&
                    pl.Shelf.MagazineId == magazineId &&
                    (location.Tag == null || location.Tag == pl.Tag)
                ) ?? throw new NotFoundException("152");

            if (itemLocation.Quantity < location.Quantity)
                throw new BadRequestException("182");

            if (itemLocation.Quantity != location.Quantity)
                itemLocation.Quantity -= location.Quantity;
            else
                _context.Remove(itemLocation);
        }

    }

}
