namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/states")]
    public class StateController(IStateService stateService)
        : ZubeldiaControllerBase
    {
        [HttpGet("{countryId}/dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetByCountryIdAsync(int countryId) => await stateService.GetByCountryIdAsync(countryId);
    }
}
