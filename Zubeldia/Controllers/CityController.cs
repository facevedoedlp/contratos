namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/cities")]
    public class CityController(ICityService cityService)
        : ZubeldiaControllerBase
    {
        [HttpGet("{stateId}/dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetByStateIdAsync(int stateId) => await cityService.GetByStateIdAsync(stateId);
    }
}
