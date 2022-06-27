using Sneakers.Models;
using AutoMapper;
using Sneakers.DTO.RequestModel;

namespace Sneakers.Extensions
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<SNEAKERS_BRAND, BrandVM>().ReverseMap();
            CreateMap<SIZE, SizeVM>().ReverseMap();
            CreateMap<SNEAKERS_TYPE, TypeVM>().ReverseMap();
            CreateMap<SNEAKERS_MODEL, ModelVM>().ReverseMap();
            CreateMap<WAREHOUSE, WarehouseVM>().ReverseMap();
            CreateMap<EMPLOYEE, EmployeeVM>().ReverseMap();
            CreateMap<SNEAKERS, SneakersVM>().ReverseMap();

        }
    }
}
