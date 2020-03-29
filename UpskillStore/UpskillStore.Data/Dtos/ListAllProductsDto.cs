namespace UpskillStore.Data.Dtos
{
    public class ListAllProductsDto
    {
        public ListAllProductsDto(int pageNumber, int numberOfElementsOnPage)
        {
            PageNumber = pageNumber;
            NumberOfElementsOnPage = numberOfElementsOnPage;
        }

        public int PageNumber { get; set; }

        public int NumberOfElementsOnPage { get; set; }
    }
}
