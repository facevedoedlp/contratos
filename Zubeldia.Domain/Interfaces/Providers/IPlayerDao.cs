namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;

    public interface IPlayerDao : IRepository<Player>
    {
        Task DeletePlayerRelationsNotInListsAsync(int playerId, IEnumerable<int>? keepPositionIds, IEnumerable<int?>? keepRelativeIds, IEnumerable<int?>? keepIdentificationIds);
        Task<SearchResultPage<Player>> GetByFiltersWithPaginationAsync(GetPlayersRequest request);
        Task<Player?> GetByIdAsync(int id, bool asNoTracking = false);
        Task<IEnumerable<KeyNameDto>> GetDropdownAsync(string? filter);
    }
}
