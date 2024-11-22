namespace Zubeldia.Services
{
    using AutoMapper;
    using FluentValidation;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    public class ContractService(IContractDao contractDao, IPdfContractProcessor pdfContractProcessor, IMapper mapper, IValidator<CreateContractRequest> contractValidator)
              : IContractService
    {
        public async Task<ValidatorResultDto> CreateAsync(CreateContractRequest request)
        {
            try
            {
                var validatorResult = await contractValidator.ValidateAsync(request);
                var response = mapper.Map<ValidatorResultDto>(validatorResult);

                if (validatorResult.IsValid)
                {
                    Contract contract = mapper.Map<Contract>(request);

                    await pdfContractProcessor.ProcessContractPdf(request.File, contract);

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
