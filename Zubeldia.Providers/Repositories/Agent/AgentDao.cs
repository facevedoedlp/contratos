namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class AgentDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Agent>(dbContext, sessionAccessor), IAgentDao
    {
        public async Task<IEnumerable<Agent>> GetAllOrderedAsync() => await dbContext.Agents.OrderBy(x => x.Name).ToListAsync();
    }
}
