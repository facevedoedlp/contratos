namespace Zubeldia.Providers.Repositories.Discipline
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class DisciplineDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Discipline>(dbContext, sessionAccessor), IDisciplineDao
    {
        public async Task<IEnumerable<Discipline>> GetDropdownAsync() => await dbContext.Disciplines.OrderBy(x => x.Name).ToListAsync();
    }
}
