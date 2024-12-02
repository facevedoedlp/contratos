namespace Zubeldia.Services.Player
{
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class PlayerService(IPlayerDao playerDao)
              : IPlayerService
    {
        public async Task<List<KeyNameDto>> GetDropdownAsync(string? filter) => (await playerDao.GetDropdownAsync(filter)).ToList();
    }
}
