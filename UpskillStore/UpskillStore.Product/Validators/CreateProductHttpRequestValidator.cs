using FluentValidation;
using System;
using UpskillStore.Product.HttpRequests;
using UpskillStore.TableStorage.Repositories;
using UpskillStore.Utils.Result;

namespace UpskillStore.Product.Validators
{
    public class CreateProductHttpRequestValidator : AbstractValidator<CreateProductHttpRequest>
    {
        public CreateProductHttpRequestValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Description).NotNull();

            RuleFor(x => x.CategoryId).NotNull();

            RuleFor(x => x.CategoryId)
                .MustAsync(async (categoryId, cancellation) =>
                {
                    var result = await categoryRepository.CheckIfExists(categoryId);
                    return result.IsSuccessful;
                })
                .When(x => !string.IsNullOrEmpty(x.CategoryId))
                .WithMessage("Category with provided id not exists");
        }
    }
}
