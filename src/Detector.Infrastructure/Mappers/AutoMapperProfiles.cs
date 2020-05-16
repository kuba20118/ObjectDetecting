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
            CreateMap<Statistics,StatsDto>()
                .ForMember(dest => dest.Correct, opt =>
                    opt.MapFrom(src => src.FeedbackFromUser.Correct)
                    )
                .ForMember(dest => dest.Incorrect, opt =>
                    opt.MapFrom(src => src.FeedbackFromUser.Incorrect)
                    )
                .ForMember(dest => dest.NotFound, opt =>
                    opt.MapFrom(src => src.FeedbackFromUser.NotFound)
                    )
                .ForMember(dest => dest.MultipleFound, opt =>
                    opt.MapFrom(src => src.FeedbackFromUser.MultipleFound)
                    )
                .ForMember(dest => dest.IncorrectBox, opt =>
                    opt.MapFrom(src => src.FeedbackFromUser.IncorrectBox)
                    );

        }
    }
}