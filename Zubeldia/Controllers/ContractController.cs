namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    [ApiController]
    [Route("api/contract")]
    public class ContractController(IContractService contractService)
        : ZubeldiaControllerBase
    {
        [HttpGet]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Read)]
        public async Task<SearchResultPage<GetContractsDto>> GetAsync([FromQuery] GetContractsRequest request) => await contractService.GetByFiltersWithPaginationAsync(request);

        [HttpPost]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Create)]
        public async Task<ActionResult<ValidatorResultDto>> CreateAsync([FromForm] CreateContractRequest contract) => Ok(await contractService.CreateOrEdit(contract));

        [HttpPut("{id}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Update)]
        public async Task<ActionResult<ValidatorResultDto>> UpdateAsync(int id, [FromForm] CreateContractRequest contract) => Ok(await contractService.CreateOrEdit(contract));

        [HttpGet("{id:int}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Read)]
        public async Task<CreateContractRequest> GetByIdAsync(int id) => await contractService.GetByIdAsync(id);

        [HttpGet("filters")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Read)]
        public async Task<ContractFiltersResponse> GetSearchFilters(int id) => await contractService.GetSearchFiltersAsync();

        [HttpGet("types")]
        public IEnumerable<KeyNameDto> GetTypes() => contractService.GetTypes();

        [HttpGet("files/contract/{id}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Read)]
        public async Task<IActionResult> GetContractFile(int id)
        {
            var result = await contractService.GetContractFileAsync(id);

            if (!result.HasValue) return NotFound();

            var (contentType, fileStream) = result.Value;

            return File(fileStream, contentType);
        }
    }
}
