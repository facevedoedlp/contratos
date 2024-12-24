namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/states")]
    public class CityController(ICityService cityService)
        : ZubeldiaControllerBase
    {
        [HttpGet("{cityId}/dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetByStateIdAsync(int stateId) => await cityService.GetByStateIdAsync(stateId);
    }
}
