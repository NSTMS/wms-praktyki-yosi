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
        }
    }
}
