namespace Zubeldia.Providers.Repositories
{
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class ContractDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Contract>(dbContext, sessionAccessor), IContractDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
