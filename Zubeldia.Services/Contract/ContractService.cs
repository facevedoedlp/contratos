namespace Zubeldia.Services
{
    using AutoMapper;
    using FluentValidation;
    using Grogu.Domain;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Contract.GetContractDto;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    public class ContractService(IContractDao contractDao, IFileStorageService fileStorageService, IPdfContractProcessor pdfContractProcessor, IMapper mapper, IValidator<CreateContractRequest> contractValidator)
              : IContractService
    {
        public IEnumerable<KeyNameDto> GetTypes() => EnumExtension.GetKeyNameFromEnum<ContractTypeEnum>();
        public async Task<GetContractDto> GetByIdAsync(int id)
        {
            var contract = await contractDao.GetByIdAsync(id);
            return mapper.Map<GetContractDto>(contract);
        }

        public async Task<SearchResultPage<GetContractsDto>> GetByFiltersWithPaginationAsync(GetContractsRequest request)
        {
            var searchResult = await contractDao.GetByFiltersWithPaginationAsync(request);
            return mapper.Map<SearchResultPage<GetContractsDto>>(searchResult);
        }

        public async Task<ValidatorResultDto> CreateAsync(CreateContractRequest request)
        {
            try
            {
                var validatorResult = await contractValidator.ValidateAsync(request);
                var response = mapper.Map<ValidatorResultDto>(validatorResult);

                if (validatorResult.IsValid)
                {
                    string fileUri = string.Empty;
                    if (request.File != null && request.File.Length > 0) fileUri = await fileStorageService.SaveFileAsync(request.File, "Contracts");

                    Contract contract = mapper.Map<Contract>(request);

                    if (request.Type != ContractTypeEnum.Afa) await pdfContractProcessor.ProcessContractPdf(request.File, contract);

                    contract.File = fileUri;

                    await contractDao.AddAsync(contract);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
