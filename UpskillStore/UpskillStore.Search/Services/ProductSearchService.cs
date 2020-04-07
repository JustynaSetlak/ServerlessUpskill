using Microsoft.Azure.Search.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpskillStore.Search.Dtos;
using UpskillStore.Search.Models;

namespace UpskillStore.Search.Services
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly ISearchService _searchService;

        public ProductSearchService(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task MergeOrUpload(ProductSearchDto productSearchDto)
        {
            var productToUpload = new Product(
                productSearchDto.Id, 
                productSearchDto.Name, 
                productSearchDto.Description, 
                productSearchDto.CategoryName);

            await _searchService.MergeOrUpload(productToUpload);
        }


        public async Task<List<ProductSearchDto>> Search(ProductSearchParamsDto productSearchParams)
        {
            var searchParameters = BuildSearchBarameters(productSearchParams);

            var result = await _searchService.Search<Product>(productSearchParams.ProductName, searchParameters);

            var mappedResult = result
                .Select(x => new ProductSearchDto(x.Id, x.Name, x.Description, x.Category))
                .ToList();

            return mappedResult;
        }

        private SearchParameters BuildSearchBarameters(ProductSearchParamsDto productSearchParams)
        {
            var filter = string.Empty;

            if (!string.IsNullOrEmpty(productSearchParams.CategoryName))
            {
                var categoryFilter = $"{nameof(Product.Category)} eq '{productSearchParams.CategoryName}'";
                filter = $"{filter} and {categoryFilter}";
            }

            var orderExpression = new List<string> { $"{nameof(Product.Name)} asc" };

            var parameters = new SearchParameters
            {
                OrderBy = orderExpression,
                Skip = (productSearchParams.PageNumber - 1) * productSearchParams.NumberOfElementsOnPage,
                Top = productSearchParams.NumberOfElementsOnPage,
                Filter = filter,
            };

            return parameters;
        }
    }
}
