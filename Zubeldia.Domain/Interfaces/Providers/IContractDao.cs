namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Threading.Tasks;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;

    public interface IContractDao : IRepository<Contract>
    {
        Task<SearchResultPage<Contract>> GetByFiltersWithPaginationAsync(GetContractsRequest request);
        Task<Contract?> GetByIdAsync(int id);
        Task<string?> GetFileAsync(int id);
    }
}
