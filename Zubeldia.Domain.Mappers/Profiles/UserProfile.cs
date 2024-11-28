namespace Zubeldia.Domain.Mappers.Profiles
{
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Dtos.Models.User;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<User, UserDto>();
        }
    }
}
