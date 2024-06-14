namespace Demo.Helpers.QueryObjects
{
    public class ProductQueryObject
    {
        public string ProductName { get; set; } = string.Empty;

        public double? ProductPriceFrom { get; set; }

        public double? ProductPriceTo { get; set; }

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}
