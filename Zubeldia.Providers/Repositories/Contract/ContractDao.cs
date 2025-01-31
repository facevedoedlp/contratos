namespace Zubeldia.Providers.Repositories
{
    using Grogu.Domain;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Commons.Extensions;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;

    public class ContractDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor)
        : Repository<Contract>(dbContext, sessionAccessor), IContractDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<string?> GetFileAsync(int id) => await dbContext.Contracts.Where(x => x.Id == id).Select(x => x.File).SingleOrDefaultAsync();

        public async Task<Contract?> GetByIdAsync(int id)
        {
            return await dbContext.Contracts
                .Include(e => e.Salaries)
                    .ThenInclude(e => e.Currency)
                .Include(e => e.Objectives)
                    .ThenInclude(e => e.Currency)
                .Include(e => e.Trajectories)
                    .ThenInclude(e => e.Currency)
                .Include(e => e.Player)
                    .ThenInclude(e => e.Identifications)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<SearchResultPage<Contract>> GetByFiltersWithPaginationAsync(GetContractsRequest request)
        {
            var contracts = GetByFilters(request);
            var resultPage = await contracts.Skip((request.Page - 1) * request.TotalRecordPage)
                                                                     .Take(request.TotalRecordPage)
                                                                     .ToListAsync();

            return new SearchResultPage<Contract>(contracts.Count(), resultPage);
        }

        public async Task DeleteContractRelationsNotInListsAsync(int contractId, IEnumerable<int?>? keepObjectiveIds, IEnumerable<int?>? keepSalaryIds, IEnumerable<int?>? keepTrajectoryIds)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                if (keepObjectiveIds != null)
                {
                    var validObjectiveIds = keepObjectiveIds.Where(id => id.HasValue).Select(id => id.Value);
                    var objectivesToDelete = dbContext.Set<ContractObjective>()
                        .Where(o => o.ContractId == contractId && !validObjectiveIds.Contains(o.Id));
                    dbContext.Set<ContractObjective>().RemoveRange(objectivesToDelete);
                }

                if (keepSalaryIds != null)
                {
                    var validSalaryIds = keepSalaryIds.Where(id => id.HasValue).Select(id => id.Value);
                    var salariesToDelete = dbContext.Set<ContractSalary>()
                        .Where(s => s.ContractId == contractId && !validSalaryIds.Contains(s.Id));
                    dbContext.Set<ContractSalary>().RemoveRange(salariesToDelete);
                }

                if (keepTrajectoryIds != null)
                {
                    var validTrajectoryIds = keepTrajectoryIds.Where(id => id.HasValue).Select(id => id.Value);
                    var trajectoriesToDelete = dbContext.Set<ContractTrajectory>()
                        .Where(t => t.ContractId == contractId && !validTrajectoryIds.Contains(t.Id));
                    dbContext.Set<ContractTrajectory>().RemoveRange(trajectoriesToDelete);
                }

                if (keepObjectiveIds != null || keepSalaryIds != null || keepTrajectoryIds != null)
                {
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private IQueryable<Contract> GetByFilters(GetContractsRequest request)
        {
            string orderField = request.SortingProperty.HasValue
                ? request.SortingProperty.Value.ToString()
                : ContractOrderPropertiesEnum.RemainingMonths.ToString();
            string orderMode = request.Sorting.HasValue
                ? request.Sorting.Value.ToString().ToUpper()
                : SortEnum.Asc.ToString().ToUpper();
            var today = DateTime.Today;
            var query = dbContext.Contracts
                .Include(x => x.Player)
                    .ThenInclude(x => x.Identifications)
                .Include(x => x.Salaries)
                    .ThenInclude(x => x.Currency)
                .Select(c => new
                {
                    Contract = c,
                    RemainingMonths = EF.Functions.DateDiffMonth(
                        today > c.StartDate ? today : c.StartDate,
                        c.EndDate),
                    CurrencyCode = c.Salaries.Any() ? c.Salaries.FirstOrDefault().Currency.Code : string.Empty,
                })
                .WhereIf(!string.IsNullOrEmpty(request.SearchText), x =>
                    x.Contract.Player.FirstName.ToUpper().Contains(request.SearchText.ToUpper()) ||
                    x.Contract.Player.LastName.ToUpper().Contains(request.SearchText.ToUpper()) ||
                    x.Contract.Player.Identifications.Any(x => x.Number.ToUpper().Contains(request.SearchText.ToUpper())) ||
                    x.RemainingMonths.ToString().Contains(request.SearchText))
                .WhereIf(request.Type.HasValue, x => x.Contract.Type == request.Type)
                .WhereIf(request.CurrencyId.HasValue, x => (x.Contract.Salaries.Any() && x.Contract.Salaries.FirstOrDefault().CurrencyId == request.CurrencyId))
                .AsNoTracking();

            if (request.SortingProperty == ContractOrderPropertiesEnum.RemainingMonths || !request.SortingProperty.HasValue)
            {
                return orderMode == "DESC"
                    ? query.OrderByDescending(x => x.RemainingMonths)
                          .ThenBy(x => x.Contract.Id)
                          .Select(x => x.Contract)
                    : query.OrderBy(x => x.RemainingMonths)
                          .ThenBy(x => x.Contract.Id)
                          .Select(x => x.Contract);
            }
            else if (request.SortingProperty == ContractOrderPropertiesEnum.PlayerFullName)
            {
                return orderMode == "DESC"
                    ? query.OrderByDescending(x => x.Contract.Player.FirstName)
                          .ThenByDescending(x => x.Contract.Player.LastName)
                          .Select(x => x.Contract)
                    : query.OrderBy(x => x.Contract.Player.FirstName)
                          .ThenBy(x => x.Contract.Player.LastName)
                          .Select(x => x.Contract);
            }
            else if (request.SortingProperty == ContractOrderPropertiesEnum.CurrencyCode)
            {
                return orderMode == "DESC"
                    ? query.OrderByDescending(x => x.CurrencyCode)
                          .ThenBy(x => x.Contract.Id)
                          .Select(x => x.Contract)
                    : query.OrderBy(x => x.CurrencyCode)
                          .ThenBy(x => x.Contract.Id)
                          .Select(x => x.Contract);
            }

            return query.Select(x => x.Contract).OrderBy($"{orderField} {orderMode}");
        }
    }
}
