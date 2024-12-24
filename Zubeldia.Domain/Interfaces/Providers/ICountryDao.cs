namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface ICountryDao : IRepository<Country>
    {
        Task<IEnumerable<Country>> GetDropdownAsync();
    }
}
