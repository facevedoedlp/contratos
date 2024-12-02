namespace Zubeldia.Domain.Interfaces.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Contract.GetContractDto;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Dtos.Models.Commons;

    public interface IContractService
    {
        Task<ValidatorResultDto> CreateAsync(CreateContractRequest request);
        Task<SearchResultPage<GetContractsDto>> GetByFiltersWithPaginationAsync(GetContractsRequest request);
        Task<GetContractDto> GetByIdAsync(int id);
        Task<(string ContentType, Stream FileStream)?> GetContractFileAsync(int id);
        IEnumerable<KeyNameDto> GetTypes();
    }
}
