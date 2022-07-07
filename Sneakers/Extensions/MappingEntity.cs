using Sneakers.Models;
using AutoMapper;
using Sneakers.DTO.RequestModel;
using TeamControlV2.DTO.ResponseModels.Inner;
using Sneakers.DTO.ResponseModel.Inner;

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
            CreateMap<BRAND_VIEW_MODEL, SNEAKERS_BRAND>().ReverseMap();
            CreateMap<TYPE_VIEW_MODEL, SNEAKERS_TYPE>().ReverseMap();
            CreateMap<MODEL_VIEW_MODEL, SNEAKERS_MODEL>().ReverseMap();
            CreateMap<SIZE_VIEW_MODEL, SIZE>().ReverseMap();


        }
    }
}
