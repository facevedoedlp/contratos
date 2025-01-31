namespace Zubeldia.Domain.Mappers.Profiles
{
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;

    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, GetPlayersResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.GetIdNumber()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.GetCategory()))
                .ForMember(dest => dest.Nacionality, opt => opt.MapFrom(src => src.GetNacionality()))
                .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.GetPositions()))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.GetDiscipline()));

            CreateMap<SearchResultPage<Player>, SearchResultPage<GetPlayersResponse>>();

            CreateMap<CreatePlayerRequest, Player>()
            .ForMember(dest => dest.Photo, opt => opt.Ignore())
            .ForMember(dest => dest.Positions, opt => opt.MapFrom((src, dest) => src.Positions != null ? src.Positions.Select(positionId => new PlayerPosition { PositionId = positionId, }).ToList() : new List<PlayerPosition>()))
            .ReverseMap()
            .ForMember(dest => dest.Photo, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Positions, opt => opt.MapFrom((src, dest) => src.Positions != null ? src.Positions.Select(pp => pp.PositionId).ToList() : null));

            CreateMap<CreatePlayerAddressRequest, PlayerAddress>().ReverseMap();

            CreateMap<CreatePlayerIdentificationRequest, PlayerIdentification>()
                .ForMember(dest => dest.FrontPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.BackPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.PlayerId, opt => opt.Condition((src, dest, destValue) => destValue != 0))
                .ReverseMap()
                .ForMember(dest => dest.FrontPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.BackPhoto, opt => opt.Ignore());

            CreateMap<CreatePlayerHealthcarePlanRequest, PlayerHealthcarePlan>()
                .ForMember(dest => dest.FrontPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.BackPhoto, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.FrontPhoto, opt => opt.Ignore())
                .ForMember(dest => dest.BackPhoto, opt => opt.Ignore());

            CreateMap<CreatePlayerRelativeRequest, PlayerRelative>().ReverseMap();
            CreateMap<CreatePlayerAddressRequest, PlayerAddress>().ReverseMap();
        }
    }
}