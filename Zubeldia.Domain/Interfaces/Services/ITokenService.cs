namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Entities;

    public interface ITokenService
    {
        string GenerateToken(int userId, string firstName, string lastName, string email, IEnumerable<Permission> permissions);
    }
}
