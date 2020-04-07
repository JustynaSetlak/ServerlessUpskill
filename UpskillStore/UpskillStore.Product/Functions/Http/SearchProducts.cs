using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using UpskillStore.Common.Constants;
using UpskillStore.Product.HttpRequests;
using UpskillStore.Search.Services;
using UpskillStore.Search.Dtos;

namespace UpskillStore.Product.Functions.Http
{
    public class SearchProducts
    {
        private readonly IProductSearchService _productSearchService;

        public SearchProducts(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        [FunctionName(nameof(SearchProducts))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.GET)] SearchProductsHttpRequest searchProductsHttpRequest)
        {
            var searchParams = new ProductSearchParamsDto(
                searchProductsHttpRequest.ProductName, 
                searchProductsHttpRequest.CategoryName, 
                searchProductsHttpRequest.PageNumber, 
                searchProductsHttpRequest.NumberOfElementsOnPage);

            var result = await _productSearchService.Search(searchParams);

            return new OkObjectResult(result);
        }
    }
}
