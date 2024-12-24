namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface IStateService
    {
        Task<IEnumerable<KeyNameDto>> GetByCountryIdAsync(int countryId);
    }
}
