namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface IDisciplineDao : IRepository<Discipline>
    {
        Task<IEnumerable<Discipline>> GetDropdownAsync();
    }
}
