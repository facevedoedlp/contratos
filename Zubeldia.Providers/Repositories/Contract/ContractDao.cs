namespace Zubeldia.Providers.Repositories
{
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
                .Include(e => e.Player)
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
                .Select(c => new
                {
                    Contract = c,
                    RemainingMonths = EF.Functions.DateDiffMonth(
                        today > c.StartDate ? today : c.StartDate,
                        c.EndDate),
                })
                .WhereIf(!string.IsNullOrEmpty(request.SearchText), x =>
                    x.Contract.Player.FirstName.ToUpper().Contains(request.SearchText.ToUpper()) ||
                    x.Contract.Player.LastName.ToUpper().Contains(request.SearchText.ToUpper()) ||
                    x.Contract.Player.DocumentNumber.ToUpper().Contains(request.SearchText.ToUpper()) ||
                    x.RemainingMonths.ToString().Contains(request.SearchText))
                .AsNoTracking();

            return (request.SortingProperty == ContractOrderPropertiesEnum.RemainingMonths || !request.SortingProperty.HasValue)
                ? (orderMode == "DESC"
                    ? query.OrderByDescending(x => x.RemainingMonths.HasValue)
                          .ThenByDescending(x => x.RemainingMonths)
                          .Select(x => x.Contract)
                    : query.OrderByDescending(x => x.RemainingMonths.HasValue)
                          .ThenBy(x => x.RemainingMonths)
                          .Select(x => x.Contract))
                : query.Select(x => x.Contract).OrderBy($"{orderField} {orderMode}");
        }
    }
}
