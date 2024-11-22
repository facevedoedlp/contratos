namespace Zubeldia.Domain.Interfaces.Providers
{
    using Zubeldia.Domain.Entities;

    public interface IUserDao : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsEmailTaken(string email);
    }
}
