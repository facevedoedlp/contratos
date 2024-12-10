namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface ICurrencyService
    {
        Task<IEnumerable<KeyNameDto>> GetDropdownAsync();
    }
}
