namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Commons.Extensions;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class CurrencyDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Currency>(dbContext, sessionAccessor), ICurrencyDao
    {
        public async Task<IEnumerable<Currency>> GetDropdownAsync() => await dbContext.Currencies.OrderBy(x => x.Code).ToListAsync();
    }
}
