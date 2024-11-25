namespace Zubeldia.Domain.Mappers.Profiles
{
    using AutoMapper;
    using Zubeldia.Domain.Entities.User;
    using Zubeldia.Dtos.Models.User;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
        }
    }
}
