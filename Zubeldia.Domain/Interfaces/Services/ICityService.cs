namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface ICityService
    {
        Task<IEnumerable<KeyNameDto>> GetByStateIdAsync(int stateId);
    }
}
