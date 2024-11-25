namespace Zubeldia.Providers.Repositories.User
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class UserDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<User>(dbContext, sessionAccessor), IUserDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<bool> IsEmailTakenAsync(string email) => await dbContext.Users.AnyAsync(u => u.Email.ToUpper() == email.ToUpper());
        public async Task<User?> GetByEmailAsync(string email) => await dbContext.Users.SingleOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            return await dbContext.UserRoles
                .Include(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(r => r.Permission)
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
