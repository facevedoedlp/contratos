namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Contract.GetContractDto;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    [ApiController]
    [Route("api/contract")]
    public class ContractController(IContractService contractService)
        : ZubeldiaControllerBase
    {
        [HttpGet]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.View)]
        public async Task<SearchResultPage<GetContractsDto>> GetAsync([FromQuery] GetContractsRequest request) => await contractService.GetByFiltersWithPaginationAsync(request);

        [HttpGet("{id:int}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.View)]
        public async Task<GetContractDto> GetByIdAsync(int id) => await contractService.GetByIdAsync(id);

        [HttpPost]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Create)]
        public async Task<ActionResult<ValidatorResultDto>> PostAsync([FromForm] CreateContractRequest contract) => Ok(await contractService.CreateAsync(contract));
    }
}
