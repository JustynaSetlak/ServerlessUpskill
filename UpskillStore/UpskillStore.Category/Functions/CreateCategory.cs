using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using UpskillStore.Common.Constants;
using UpskillStore.Category.HttpRequests;
using UpskillStore.TableStorage.Repositories;
using FluentValidation;
using UpskillStore.TableStorage.Dtos;

namespace UpskillStore.Category.Functions
{
    public class CreateCategory
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryHttpRequest> _validator;

        public CreateCategory(ICategoryRepository categoryRepository, IValidator<CreateCategoryHttpRequest> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        [FunctionName(nameof(CreateCategory))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.POST)] CreateCategoryHttpRequest createCategoryHttpRequest)
        {
            var validationResult = await _validator.ValidateAsync(createCategoryHttpRequest);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var categoryToAdd = new CategoryToAddDto(createCategoryHttpRequest.Name, createCategoryHttpRequest.Description);

            var result = await _categoryRepository.CreateCategoryAsync(categoryToAdd);

            if (!result.IsSuccessful)
            {
                return new BadRequestObjectResult(result.Value);
            }

            return new OkObjectResult("");
        }
    }
}
