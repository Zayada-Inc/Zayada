namespace Domain.Specifications.Users
{
    public class UsersParam
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 0;
        private int _pageSize = 9;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? Sort { get; set; }
        private string _search;
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
