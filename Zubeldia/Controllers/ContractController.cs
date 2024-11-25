namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    [ApiController]
    [Route("api/contract")]
    public class ContractController(IContractService contractService)
        : ZubeldiaControllerBase
    {
        [HttpPost]
        [Authorize(PermissionResourceTypeEnum.Contracts, PermissionActionEnum.Create)]
        public async Task<ActionResult<ValidatorResultDto>> CreateAsync([FromForm] CreateContractRequest contract)
        {
            var response = await contractService.CreateAsync(contract);
            return Ok(response);
        }
    }
}
