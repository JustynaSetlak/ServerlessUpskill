namespace UpskillStore.Search.Dtos
{
    public class ProductSearchParamsDto
    {
        public ProductSearchParamsDto(string productName, string categoryName, int pageNumber, int numberOfElementsOnPage)
        {
            ProductName = productName;
            CategoryName = categoryName;
            PageNumber = pageNumber;
            NumberOfElementsOnPage = numberOfElementsOnPage;
        }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public int PageNumber { get; set; }

        public int? NumberOfElementsOnPage { get; set; }
    }
}
