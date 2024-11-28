namespace Zubeldia.Domain.Entities.Base
{
    public class SearchResultPage<T>
    {
        public SearchResultPage(int totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }

        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }
    }
}
