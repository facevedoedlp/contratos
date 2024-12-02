namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Authorization;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/player")]
    public class PlayerController(IPlayerService playerService)
        : ZubeldiaControllerBase
    {
        [HttpGet("dropdown/{filter?}")]
        public async Task<List<KeyNameDto>> GetDropdownAsync(string? filter = null) => await playerService.GetDropdownAsync(filter);
    }
}
