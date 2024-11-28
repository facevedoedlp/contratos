namespace Zubeldia.Domain.Dtos.Commons.Searches
{
    public class Pagination
    {
        private int page;

        public int Page
        {
            get
            {
                return page == 0 ? 1 : page;
            }
            set
            {
                page = value;
            }
        }
    }
}
