namespace Zubeldia.Providers.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Commons.Extensions;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;
    using Zubeldia.Domain.Interfaces.Providers;

    public class ContractDao(ZubeldiaDbContext dbContext, ISessionAccessor sessionAccessor, IConfiguration configuration)
        : Repository<Contract>(dbContext, sessionAccessor), IContractDao
    {
        private readonly ZubeldiaDbContext dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly int totalRecordsPerPage = configuration.GetSection("Searchs").GetSection("TotalRecordsPerPage").Get<int>();

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
            var resultPage = await contracts.Skip((request.Page - 1) * totalRecordsPerPage)
                                                                     .Take(totalRecordsPerPage)
                                                                     .ToListAsync();

            return new SearchResultPage<Contract>(contracts.Count(), resultPage);
        }

        private IQueryable<Contract> GetByFilters(GetContractsRequest request)
        {
            string orderField = request.SortingProperty.HasValue ? request.SortingProperty.Value.ToString() : nameof(request.Title);
            string orderMode = request.Sorting.HasValue ? request.Sorting.Value.ToString().ToUpper() : SortEnum.Desc.ToString().ToUpper();

            return dbContext.Contracts
                .WhereIf(!string.IsNullOrEmpty(request.Title), x => x.Title.ToUpper().Contains(request.Title.ToUpper()))
                .AsNoTracking()
                .OrderBy($"{orderField} {orderMode}");
        }
    }
}
