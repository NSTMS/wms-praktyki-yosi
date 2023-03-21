using AutoMapper;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Models;

namespace wms_praktyki_yosi_api
{
    public class MagazinesMappingProfile : Profile
    {
        public MagazinesMappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductLocations, ReturnProductLocationDto>()
                .ForMember(p => p.Position, m => m.MapFrom(r => r.Shelf.Position));

            CreateMap<ProductLocationDto, ProductLocations>();
            /*CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, c => c.MapFrom(r => r.Name));*/
        }
    }
}
