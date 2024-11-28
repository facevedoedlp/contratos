namespace Zubeldia.Services.Session
{
    using AutoMapper;
    using BCrypt.Net;
    using FluentValidation;
    using Grogu.Domain;
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Dtos.Models.User;

    public class AuthenticateService(IUserDao userDao, IMapper mapper, IValidator<RegisterUserRequest> userValidator, ITokenService tokenService)
        : IAuthenticateService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest loginDto)
        {
            var user = await userDao.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid credentials",
                };
            }

            if (!BCrypt.Verify(loginDto.Password, user.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid credentials",
                };
            }

            // Obtén los permisos del usuario
            var permissions = await userDao.GetUserPermissionsAsync(user.Id);

            var token = tokenService.GenerateToken(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                permissions
            );

            var userDto = mapper.Map<UserDto>(user);
            userDto.Permissions = permissions.Select(p => $"{p.Resource.DisplayValue()}.{p.Action.DisplayValue()}").ToList();

            return new LoginResponse
            {
                Success = true,
                Access = token,
                User = userDto,
            };
        }

        public async Task<ValidatorResultDto> RegisterAsync(RegisterUserRequest userDto)
        {
            var validatorResult = await userValidator.ValidateAsync(userDto);
            var response = mapper.Map<ValidatorResultDto>(validatorResult);

            if (validatorResult.IsValid)
            {
                userDto.Password = BCrypt.HashPassword(userDto.Password, BCrypt.GenerateSalt(12));
                User user = mapper.Map<User>(userDto);

                await userDao.AddAsync(user);
            }

            return response;
        }

        public async Task<IEnumerable<Permission>> GetUserPermissions(int userId) => await userDao.GetUserPermissionsAsync(userId);
    }
}
