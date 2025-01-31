namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Commons.Extensions;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Player;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;

    public class PlayerDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Player>(dbContext, sessionAccessor), IPlayerDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IEnumerable<KeyNameDto>> GetDropdownAsync(string? filter)
        {
            return await dbContext.Players
                .Include(x => x.Identifications)
                .WhereIf(!string.IsNullOrEmpty(filter), x =>
                    x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
                    x.LastName.ToUpper().Contains(filter.ToUpper()) ||
                    x.Identifications.Any(x => x.Number.ToUpper().Contains(filter.ToUpper())))
                .Select(x => new KeyNameDto
                {
                    Name = $"{x.FirstName} {x.LastName}",
                    Id = x.Id,
                }).AsNoTracking().ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(int id, bool asNoTracking = false)
        {
            IQueryable<Player> query = dbContext.Players
                .Include(e => e.Positions)
                .Include(e => e.Identifications)
                .Include(e => e.Relatives)
                .Include(e => e.Address)
                .Include(e => e.HealthcarePlan);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task DeletePlayerRelationsNotInListsAsync(int playerId, IEnumerable<int>? keepPositionIds, IEnumerable<int?>? keepRelativeIds, IEnumerable<int?>? keepIdentificationIds)
        {
            if (keepPositionIds != null)
            {
                var positionsToDelete = await dbContext.Set<PlayerPosition>()
                    .Where(p => p.PlayerId == playerId && !keepPositionIds.Contains(p.PositionId))
                    .ToListAsync();
                dbContext.Set<PlayerPosition>().RemoveRange(positionsToDelete);
            }

            if (keepRelativeIds != null)
            {
                var relativesToDelete = await dbContext.Set<PlayerRelative>()
                    .Where(r => r.PlayerId == playerId && !keepRelativeIds.Contains(r.Id))
                    .ToListAsync();
                dbContext.Set<PlayerRelative>().RemoveRange(relativesToDelete);
            }

            if (keepIdentificationIds != null)
            {
                var identificationsToDelete = await dbContext.Set<PlayerIdentification>()
                    .Where(i => i.PlayerId == playerId && !keepIdentificationIds.Contains(i.Id))
                    .ToListAsync();
                dbContext.Set<PlayerIdentification>().RemoveRange(identificationsToDelete);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<SearchResultPage<Player>> GetByFiltersWithPaginationAsync(GetPlayersRequest request)
        {
            var players = GetByFilters(request);
            var resultPage = await players.Skip((request.Page - 1) * request.TotalRecordPage)
                                                                     .Take(request.TotalRecordPage)
                                                                     .ToListAsync();

            return new SearchResultPage<Player>(players.Count(), resultPage);
        }

        private IQueryable<Player> GetByFilters(GetPlayersRequest request)
        {
            string orderField = request.SortingProperty.HasValue ? request.SortingProperty.Value.ToString() : PlayerOrderPropertiesEnum.LastName.ToString();
            string orderMode = request.Sorting.HasValue ? request.Sorting.Value.ToString().ToUpper() : SortEnum.Desc.ToString().ToUpper();

            return dbContext.Players.Include(x => x.Identifications)
                                    .Include(x => x.Discipline)
                                    .Include(x => x.Category)
                                    .Include(x => x.Positions)
                                        .ThenInclude(x => x.Position)
                                    .OrderBy($"{orderField} {orderMode}");
        }
    }
}
