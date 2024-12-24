namespace Zubeldia.Providers.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class HealthcarePlanDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<HealthcarePlan>(dbContext, sessionAccessor), IHealthcarePlanDao
    {
        public async Task<IEnumerable<HealthcarePlan>> GetDropdownAsync() => await dbContext.HealthcarePlans.OrderBy(x => x.Name).ToListAsync();
    }
}
