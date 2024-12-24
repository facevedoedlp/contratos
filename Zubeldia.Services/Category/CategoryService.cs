namespace Zubeldia.Services.Category
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class CategoryService(ICategoryDao categoryDao, IMapper mapper)
          : ICategoryService
    {
        public async Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId)
        {
            var categories = await categoryDao.GetByDisciplineIdAsync(disciplineId);
            return mapper.Map<List<KeyNameDto>>(categories);
        }
    }
}
