using AutoMapper;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models.DocumentModels;
using wms_praktyki_yosi_api.Models.MagazineModels;
using wms_praktyki_yosi_api.Models.ProductModels;
using wms_praktyki_yosi_api.Models.StandingOrderModels;

namespace wms_praktyki_yosi_api
{
    public class MagazinesMappingProfile : Profile
    {
        public MagazinesMappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductLocations, ReturnProductLocationDto>()
                .ForMember(p => p.Position, m => m.MapFrom(r => r.Shelf.Position))
                .ForMember(m => m.MagazineId, c => c.MapFrom(r => r.Shelf.MagazineId));

            CreateMap<ProductLocationDto, ProductLocations>();
            CreateMap<ProductLocations, ProductLocationDto>()
                .ForMember(p => p.Position, m => m.MapFrom(r => r.Shelf.Position))
                .ForMember(m => m.MagazineId, c => c.MapFrom(r => r.Shelf.MagazineId));

            CreateMap<Magazine, MagazineDto>();
            CreateMap<MagazineDto, Magazine>();
            CreateMap<AddDocumentDto, Document>();

            CreateMap<Document, DocumentDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(d => d.Id.ToString()))
                .ForMember(d => d.QuantityDone, opt => opt.MapFrom(d => d.Items.Sum(i => i.QuantityDone)))
                .ForMember(d => d.TotalQuantity, opt => opt.MapFrom(d => d.Items.Sum(i => i.Quantityplaned)));

            CreateMap<Document, DetailedDocumentDto>()
                .ForMember(d => d.QuantityDone, opt => opt.MapFrom(d => d.Items.Sum(i => i.QuantityDone)))
                .ForMember(d => d.TotalQuantity, opt => opt.MapFrom(d => d.Items.Sum(i => i.Quantityplaned)));

            CreateMap<DocumentItem, DocumentItemDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(d => d.Id.ToString()))
               .ForMember(d => d.ProductName, opt => opt.MapFrom(d => d.Product.ProductName))
               .ForMember(d => d.Status, opt => opt.MapFrom(d =>
                    (d.QuantityDone == 0) ?
                    "Waiting to start" :
                    (d.Quantityplaned > d.QuantityDone) ?
                    "In progress" :
                    (d.QuantityDone == d.Quantityplaned) ?
                    "Done" :
                    "To much stuff"
                ));

            CreateMap<StandingOrder, DetailedStandingOrderDto>()
                .ForMember(d => d.TotalQuantity, opt => opt.MapFrom(d => d.Items.Sum(i => i.Quantity)));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(d => d.Product.ProductName));

            CreateMap<StandingOrder, StandingOrderDto>()
                .ForMember(d => d.TotalQuantity, opt => opt.MapFrom(d => d.Items.Sum(i => i.Quantity)));
        }
    }
}
