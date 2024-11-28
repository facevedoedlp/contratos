namespace Zubeldia.Domain.Dtos.Commons.Searches
{
    using Zubeldia.Commons.Enums;

    public class SearchCriteria : Pagination
    {
        public string? SearchText { get; set; }
        public SortEnum? Sorting { get; set; }
    }
}
