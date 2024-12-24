namespace Zubeldia.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Services;

    [ApiController]
    [Route("api/categories")]
    public class CategoryController(ICategoryService categoryService)
        : ZubeldiaControllerBase
    {
        [HttpGet("{disciplineId}/dropdown")]
        public async Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId) => await categoryService.GetByDisciplineIdAsync(disciplineId);
    }
}
