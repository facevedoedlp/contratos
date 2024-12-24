namespace Zubeldia.Providers.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class StateDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<State>(dbContext, sessionAccessor), IStateDao
    {
        public async Task<IEnumerable<State>> GetByCountryId(int countryId) => await dbContext.States.Where(x => x.CountryId == countryId).OrderBy(x => x.Name).ToListAsync();
        }
    }
}
