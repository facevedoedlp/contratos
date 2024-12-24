namespace Zubeldia.Domain.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class FilterOptionsProfile : Profile
    {
        public FilterOptionsProfile()
        {
            CreateMap<Currency, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<Country, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<State, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<City, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<Category, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<Discipline, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<Position, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));

            CreateMap<HealthcarePlan, KeyNameDto>()
                .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => false));
        }
    }
}
