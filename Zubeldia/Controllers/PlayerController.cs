namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Dtos.Models.Commons;

    [ApiController]
    [Route("api/players")]
    public class PlayerController(IPlayerService playerService)
        : ZubeldiaControllerBase
    {
        [HttpGet]
        [Authorize(PermissionResourceTypeEnum.Players, PermissionActionEnum.Read)]
        public async Task<SearchResultPage<GetPlayersResponse>> GetAsync([FromQuery] GetPlayersRequest request) => await playerService.GetByFiltersWithPaginationAsync(request);
        [HttpGet("{id:int}")]
        [Authorize(PermissionResourceTypeEnum.Players, PermissionActionEnum.Read)]
        public async Task<CreatePlayerRequest> GetByIdAsync(int id) => await playerService.GetByIdAsync(id);
        [Authorize(PermissionResourceTypeEnum.Players, PermissionActionEnum.Create)]
        [HttpPost]
        public async Task<ActionResult<ValidatorResultDto>> CreateAsync([FromForm] CreatePlayerRequest player) => Ok(await playerService.CreateOrEdit(player));
        [HttpPut("{id}")]
        [Authorize(PermissionResourceTypeEnum.Players, PermissionActionEnum.Update)]
        public async Task<ActionResult<ValidatorResultDto>> UpdateAsync(int id, [FromForm] CreatePlayerRequest player) => Ok(await playerService.CreateOrEdit(player));

        [HttpGet("form/dropdowns")]
        [Authorize(PermissionResourceTypeEnum.Players, PermissionActionEnum.Read)]
        public async Task<GetPlayerFormDropdownResponse> GetFormDropdownsAsync() => await playerService.GetFormDropdownsAsync();

        [HttpGet("dropdown/{filter?}")]
        public async Task<List<KeyNameDto>> GetDropdownAsync(string? filter = null) => await playerService.GetDropdownAsync(filter);
    }
}
