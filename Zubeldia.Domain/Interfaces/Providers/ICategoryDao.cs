namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface ICategoryDao : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetByDisciplineIdAsync(int disciplineId);
    }
}
