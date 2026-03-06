using AutoMapper;
using WebApplicationApi.Data.Entities;
using WebApplicationApi.DTOs;

namespace WebApplicationApi.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Red, RedDTO>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Idr));

            CreateMap<RedDTO, Red>()
                .ForMember(dest => dest.Idr,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
