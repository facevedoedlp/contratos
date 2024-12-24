namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface IStateDao : IRepository<State>
    {
        Task<IEnumerable<State>> GetByCountryId(int countryId);
    }
}
