namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using Zubeldia.Domain.Entities;

    public interface IUserDao : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
