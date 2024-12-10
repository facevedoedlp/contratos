namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/currency")]
    public class CurrencyController(ICurrencyService currencyService)
        : ZubeldiaControllerBase
    {
        [HttpGet("dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetDropdownAsync() => await currencyService.GetDropdownAsync();
    }
}
