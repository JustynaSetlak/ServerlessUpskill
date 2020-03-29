using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UpskillStore.Common.Constants;
using UpskillStore.Product.HttpRequests;
using UpskillStore.Data.Repositories;

namespace UpskillStore.Product.Functions.Http
{
    public class ListAllProducts
    {
        private readonly IProductRepository _productRepository;

        public ListAllProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [FunctionName(nameof(ListAllProducts))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.GET)] ListAllProductsHttpRequest listAllProductsHttpRequest)
        {
            //var result = _productRepository.
            return new OkObjectResult("");
        }
    }
}
