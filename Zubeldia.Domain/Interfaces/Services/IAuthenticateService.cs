namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Dtos.Models.User;

    public interface IAuthenticateService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginDto);
        Task<ValidatorResultDto> RegisterAsync(UserDto userDto);
    }
}
