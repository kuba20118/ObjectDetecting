using AutoMapper;
using Detector.Core.Domain;
using Detector.Infrastructure.Dtos;

namespace Detector.Infrastructure.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();

        }
    }
}