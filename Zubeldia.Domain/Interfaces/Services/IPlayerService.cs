namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;

    public interface IPlayerService
    {
        Task<List<KeyNameDto>> GetDropdownAsync(string? filter);
    }
}
