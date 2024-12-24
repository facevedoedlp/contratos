namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface ICityDao : IRepository<City>
    {
        Task<IEnumerable<City>> GetByStateIdAsync(int stateId);
    }
}
