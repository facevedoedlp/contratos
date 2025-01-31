namespace Zubeldia.Services
{
    using AutoMapper;
    using FluentValidation;
    using Grogu.Domain;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    public class ContractService(IContractDao contractDao, ICurrencyDao currencyDao, IFileStorageService fileStorageService, IMapper mapper, IValidator<CreateContractRequest> contractValidator)
              : IContractService
    {
        public IEnumerable<KeyNameDto> GetTypes() => EnumExtension.GetKeyNameFromEnum<ContractTypeEnum>();
        public async Task<ContractFiltersResponse> GetSearchFiltersAsync()
        {
            var currencies = await currencyDao.GetDropdownAsync();

            return new ContractFiltersResponse
            {
                Currencies = mapper.Map<IEnumerable<KeyNameDto>>(currencies),
            };
        }

        public async Task<CreateContractRequest> GetByIdAsync(int id)
        {
            var contract = await contractDao.GetByIdAsync(id);
            return mapper.Map<CreateContractRequest>(contract);
        }

        public async Task<SearchResultPage<GetContractsDto>> GetByFiltersWithPaginationAsync(GetContractsRequest request)
        {
            var searchResult = await contractDao.GetByFiltersWithPaginationAsync(request);
            return mapper.Map<SearchResultPage<GetContractsDto>>(searchResult);
        }

        public async Task<(string ContentType, Stream FileStream)?> GetContractFileAsync(int id)
        {
            var fileUri = await contractDao.GetFileAsync(id);

            if (string.IsNullOrEmpty(fileUri)) return null;

            try
            {
                var fileStream = fileStorageService.GetFileStreamAsync(fileUri);
                return ("application/pdf", fileStream);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public async Task<ValidatorResultDto> CreateOrEdit(CreateContractRequest request)
        {
            try
            {
                var validatorResult = await contractValidator.ValidateAsync(request);
                if (validatorResult.IsValid)
                {
                    if (request.Id == null || request.Id.Value == 0)
                    {
                        await SaveAsync(request);
                    }
                    else
                    {
                        await UpdateAsync(request);
                    }
                }

                var response = mapper.Map<ValidatorResultDto>(validatorResult);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateAsync(CreateContractRequest request)
        {
            var existingContract = await contractDao.GetByIdAsync(request.Id.Value);
            if (existingContract == null) throw new KeyNotFoundException($"Contract with ID {request.Id.Value} not found");

            string fileUri = existingContract.File;
            if (request.File != null && request.File.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingContract.File))
                {
                    await fileStorageService.DeleteFileAsync(existingContract.File, "Contracts");
                }

                fileUri = await fileStorageService.SaveFileAsync(request.File, "Contracts");
            }

            await contractDao.DeleteContractRelationsNotInListsAsync(
                contractId: request.Id.Value,
                keepObjectiveIds: request.Objectives?.Select(o => (int?)o.Id),
                keepSalaryIds: request.Salaries?.Select(s => (int?)s.Id),
                keepTrajectoryIds: request.Trajectories?.Select(t => (int?)t.Id)
            );

            Contract updatedContract = mapper.Map<Contract>(request);
            updatedContract.File = fileUri;

            await contractDao.UpdateAsync(updatedContract);
        }

        private async Task SaveAsync(CreateContractRequest request)
        {
            string fileUri = string.Empty;
            if (request.File != null && request.File.Length > 0) fileUri = await fileStorageService.SaveFileAsync(request.File, "Contracts");

            Contract contract = mapper.Map<Contract>(request);
            contract.File = fileUri;

            await contractDao.AddAsync(contract);
        }
    }
}
