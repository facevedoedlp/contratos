namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface ICategoryService
    {
        Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId);
    }
}
