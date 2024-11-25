namespace Zubeldia.Domain.Interfaces.Services
{
    using System.Collections.Generic;
    using Zubeldia.Domain.Dtos.Authentication;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Dtos.Models.User;

    public interface IAuthenticateService
    {
        Task<IEnumerable<Permission>> GetUserPermissions(int userId);
        Task<LoginResponse> LoginAsync(LoginRequest loginDto);
        Task<ValidatorResultDto> RegisterAsync(UserDto userDto);
    }
}
