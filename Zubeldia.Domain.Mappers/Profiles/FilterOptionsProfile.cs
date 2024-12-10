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
        }
    }
}
