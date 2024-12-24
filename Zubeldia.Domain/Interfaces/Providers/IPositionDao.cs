namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Zubeldia.Domain.Entities;

    public interface IPositionDao : IRepository<Position>
    {
        Task<IEnumerable<Position>> GetByDisciplineIdAsync(int disciplineId);
    }
}
