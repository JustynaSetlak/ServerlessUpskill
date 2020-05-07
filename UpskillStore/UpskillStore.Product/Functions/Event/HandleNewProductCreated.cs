// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using System.Threading.Tasks;
using UpskillStore.Data.Repositories;
using UpskillStore.EventPublisher.Events;
using UpskillStore.Search.Dtos;
using UpskillStore.Search.Services;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.Product.Functions.Event
{
    public class HandleNewProductCreated
    {
        private readonly IProductSearchService _productsearchService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public HandleNewProductCreated(
            IProductSearchService searchService, 
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _productsearchService = searchService;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [FunctionName(nameof(HandleNewProductCreated))]
        public async Task Run([EventGridTrigger]NewProductCreated newProductCreated)
        {
            var createdProduct = await _productRepository.GetProduct(newProductCreated.Id);

            if (!createdProduct.IsSuccessful)
            {
                return;
            }

            var categoryResult = await _categoryRepository.GetById(createdProduct.Value.CategoryId);

            if (!categoryResult.IsSuccessful)
            {
                return;
            }

            var product = createdProduct.Value;

            var productToIndex = new ProductSearchDto(product.Id, product.Name, product.Description, categoryResult.Value.Name);

            await _productsearchService.MergeOrUpload(productToIndex);
        }
    }
}
