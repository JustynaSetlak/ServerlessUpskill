using FluentValidation;
using System.Threading.Tasks;
using UpskillStore.Category.HttpRequests;
using UpskillStore.TableStorage.Repositories;

namespace UpskillStore.Category.Validators
{
    public class CreateCategoryHttpRequestValidator : AbstractValidator<CreateCategoryHttpRequest>
    {
        public CreateCategoryHttpRequestValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellation) => {
                    var dataResult = await categoryRepository.GetByPropertyValue(nameof(CreateCategoryHttpRequest.Name), name);
                    return dataResult.Value.Count == 0;
                }).WithErrorCode("Already Exists");
        }
    }
}
