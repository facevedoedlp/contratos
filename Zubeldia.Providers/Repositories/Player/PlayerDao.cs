namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Commons.Extensions;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class PlayerDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Player>(dbContext, sessionAccessor), IPlayerDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IEnumerable<KeyNameDto>> GetDropdownAsync(string? filter)
        {
            return await dbContext.Players
                .WhereIf(!string.IsNullOrEmpty(filter), x =>
                    x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
                    x.LastName.ToUpper().Contains(filter.ToUpper()) ||
                    x.DocumentNumber.ToUpper().Contains(filter.ToUpper()))
                .Select(x => new KeyNameDto
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    Id = x.Id,
                }).AsNoTracking().ToListAsync();
        }
    }
}
