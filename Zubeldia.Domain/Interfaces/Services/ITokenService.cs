namespace Zubeldia.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string[] roles);
    }
}
