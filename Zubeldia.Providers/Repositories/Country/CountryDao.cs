namespace Zubeldia.Providers.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class CountryDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Country>(dbContext, sessionAccessor), ICountryDao
    {
        public async Task<IEnumerable<Country>> GetDropdownAsync() => await dbContext.Countries.OrderBy(x => x.Name).ToListAsync();
    }
}
