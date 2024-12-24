namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface IPositionService
    {
        Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId);
    }
}
