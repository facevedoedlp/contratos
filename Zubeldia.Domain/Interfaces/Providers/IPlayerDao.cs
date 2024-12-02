namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Entities;
    public interface IPlayerDao : IRepository<Player>
    {
        Task<IEnumerable<KeyNameDto>> GetDropdownAsync(string? filter);
    }
}
