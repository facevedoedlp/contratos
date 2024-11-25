namespace Zubeldia.Services.Session
{
    using AutoMapper;
    using BCrypt.Net;
    using FluentValidation;
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Domain.Entities.User;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Dtos.Models.User;

    public class AuthenticateService(IUserDao userDao, IMapper mapper, IValidator<UserDto> userValidator, ITokenService tokenService)
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

            var token = tokenService.GenerateToken(
                user.Id.ToString(),
                new string[] { } // TODO: Roles
            );

            return new LoginResponse
            {
                Success = true,
                Token = token,
                Message = "Login successful",
            };
        }

        public async Task<ValidatorResultDto> RegisterAsync(UserDto userDto)
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
    }
}
