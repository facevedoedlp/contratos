namespace Zubeldia.Domain.Interfaces.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Dtos.Models.Commons;

    public interface IContractService
    {
        Task<ValidatorResultDto> CreateOrEdit(CreateContractRequest request);
        Task<SearchResultPage<GetContractsDto>> GetByFiltersWithPaginationAsync(GetContractsRequest request);
        Task<CreateContractRequest> GetByIdAsync(int id);
        Task<(string ContentType, Stream FileStream)?> GetContractFileAsync(int id);
        Task<ContractFiltersResponse> GetSearchFiltersAsync();
        IEnumerable<KeyNameDto> GetTypes();
    }
}
