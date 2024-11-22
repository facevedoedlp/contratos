namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Dtos.Models.Commons;

    public interface IContractService
    {
        Task<ValidatorResultDto> CreateAsync(CreateContractRequest request);
    }
}
