using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using UpskillStore.Common.Constants;
using UpskillStore.Data.Dtos;
using UpskillStore.Data.Repositories;
using UpskillStore.Product.HttpRequests;

namespace UpskillStore.Product.Functions.Http
{
    public class CreateProduct
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductHttpRequest> _validator;

        public CreateProduct(IProductRepository productRepository, IValidator<CreateProductHttpRequest> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        [FunctionName(nameof(CreateProduct))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.POST)] CreateProductHttpRequest createProductHttpRequest)
        {
            var validationResult = _validator.Validate(createProductHttpRequest);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var productToCreate = new ProductToCreateDto
            {
                Name = createProductHttpRequest.Name,
                Description = createProductHttpRequest.Description,
                CategoryId = createProductHttpRequest.CategoryId,
            };

            var result = await _productRepository.CreateProduct(productToCreate);

            if (!result.IsSuccessful)
            {
                return new BadRequestResult();
            }

            //publish event

            return new OkObjectResult(result.Value);
        }
    }
}
