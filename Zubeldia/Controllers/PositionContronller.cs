namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/positions")]
    public class PositionController(IPositionService positionService)
        : ZubeldiaControllerBase
    {
        [HttpGet("{disciplineId}/dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId) => await positionService.GetByDisciplineIdAsync(disciplineId);
    }
}
