namespace Zubeldia.Domain.Dtos.Commons.Searches
{
    public class Pagination
    {
        private int page;
        private int totalRecordPage = 10;

        public int Page
        {
            get { return page == 0 ? 1 : page; }
            set { page = value; }
        }

        public int TotalRecordPage
        {
            get { return totalRecordPage; }
            set { totalRecordPage = value > 0 ? value : 10; }
        }
    }
}
