namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class CityDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<City>(dbContext, sessionAccessor), ICityDao
    {
        public async Task<IEnumerable<City>> GetByStateIdAsync(int stateId) => await dbContext.Cities.Where(x => x.StateId == stateId).OrderBy(x => x.Name).ToListAsync();
    }
}
