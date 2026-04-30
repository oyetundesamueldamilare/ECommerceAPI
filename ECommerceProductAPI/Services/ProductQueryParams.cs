namespace ECommerceProductAPI.Services
{
    public class ProductQueryParams
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }

        // Sorting
        public string SortBy { get; set; } = "Id";   // default
        public bool SortDesc { get; set; } = false;

        // Paging
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 50 ? 50 : value < 1 ? 1 : value;
        }
    }

}
