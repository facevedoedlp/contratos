namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;

    public interface IContractDao : IRepository<Contract>
    {
        Task DeleteContractRelationsNotInListsAsync(int contractId, IEnumerable<int?>? keepObjectiveIds, IEnumerable<int?>? keepSalaryIds, IEnumerable<int?>? keepTrajectoryIds);
        Task<SearchResultPage<Contract>> GetByFiltersWithPaginationAsync(GetContractsRequest request);
        Task<Contract?> GetByIdAsync(int id);
        Task<string?> GetFileAsync(int id);
    }
}
