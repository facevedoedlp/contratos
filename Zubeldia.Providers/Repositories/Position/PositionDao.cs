namespace Zubeldia.Providers.Repositories.Position
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class PositionDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Position>(dbContext, sessionAccessor), IPositionDao
    {
        public async Task<IEnumerable<Position>> GetByDisciplineIdAsync(int disciplineId) => await dbContext.Positions.Where(x => x.DisciplineId == disciplineId).OrderBy(x => x.Name).ToListAsync();
    }
}
