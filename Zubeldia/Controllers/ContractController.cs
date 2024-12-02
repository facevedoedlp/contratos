namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Contract.GetContractDto;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;
    using Zubeldia.Services;

    [ApiController]
    [Route("api/contract")]
    public class ContractController(IContractService contractService)
        : ZubeldiaControllerBase
    {
        [HttpGet("types")]
        public IEnumerable<KeyNameDto> GetTypes() => contractService.GetTypes();

        [HttpGet]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.View)]
        public async Task<SearchResultPage<GetContractsDto>> GetAsync([FromQuery] GetContractsRequest request) => await contractService.GetByFiltersWithPaginationAsync(request);

        [HttpGet("{id:int}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.View)]
        public async Task<GetContractDto> GetByIdAsync(int id) => await contractService.GetByIdAsync(id);
        [HttpGet("files/contract/{id}")]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.View)]
        public async Task<IActionResult> GetContractFile(int id)
        {
            var result = await contractService.GetContractFileAsync(id);

            if (!result.HasValue) return NotFound();

            var (contentType, fileStream) = result.Value;

            return File(fileStream, contentType);
        }

        [HttpPost]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Create)]
        public async Task<ActionResult<ValidatorResultDto>> PostAsync([FromForm] CreateContractRequest contract) => Ok(await contractService.CreateAsync(contract));
    }
}
