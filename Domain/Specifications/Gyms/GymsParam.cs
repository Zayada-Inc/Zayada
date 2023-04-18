namespace Domain.Specifications.Gyms
{
    public class GymsParam
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 0;
        private string _address;
        private int _pageSize = 9;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SearchByAddress
        {
            get => _address;
            set => _address = value.ToLower();
        }

        public int? GymId { get; set; }
        public string? Sort { get; set; }
        private string? _search = string.Empty;
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
