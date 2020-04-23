using AutoMapper;
using Detector.Core.Domain;
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Image, ImageDto>().ReverseMap();
                // .ForMember(dest => dest.ImageProcessed, opt =>
                //     opt.MapFrom(src => src.ImageOriginal));
        }
    }
}