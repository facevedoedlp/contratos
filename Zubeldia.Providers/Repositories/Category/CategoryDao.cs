namespace Zubeldia.Providers.Repositories.Category
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class CategoryDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Category>(dbContext, sessionAccessor), ICategoryDao
    {
        public async Task<IEnumerable<Category>> GetByDisciplineIdAsync(int disciplineId) => await dbContext.Categories.Where(x => x.DisciplineId == disciplineId).OrderBy(x => x.Name).ToListAsync();
    }
}
