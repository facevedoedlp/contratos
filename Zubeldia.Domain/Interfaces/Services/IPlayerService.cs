namespace Zubeldia.Domain.Interfaces.Services
{
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Dtos.Models.Commons;

    public interface IPlayerService
    {
        Task<ValidatorResultDto> CreateOrEdit(CreatePlayerRequest request);
        Task<SearchResultPage<GetPlayersResponse>> GetByFiltersWithPaginationAsync(GetPlayersRequest request);
        Task<CreatePlayerRequest> GetByIdAsync(int id);
        Task<List<KeyNameDto>> GetDropdownAsync(string? filter);
        Task<GetPlayerFormDropdownResponse> GetFormDropdownsAsync();
    }
}
