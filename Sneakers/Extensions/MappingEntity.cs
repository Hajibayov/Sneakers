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

        }
    }
}
