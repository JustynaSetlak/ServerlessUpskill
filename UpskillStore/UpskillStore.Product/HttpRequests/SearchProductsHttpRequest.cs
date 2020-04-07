namespace UpskillStore.Product.HttpRequests
{
    public class SearchProductsHttpRequest
    {
        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public int PageNumber { get; set; }

        public int NumberOfElementsOnPage { get; set; }
    }
}
