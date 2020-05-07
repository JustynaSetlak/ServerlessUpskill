using FluentValidation;
using System;
using UpskillStore.Data.Repositories;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.HttpRequests;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.Promotion.Validators
{
    public class CreatePromotionHttpRequestValidator : AbstractValidator<CreatePromotionHttpRequest>
    {
        public CreatePromotionHttpRequestValidator(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            RuleFor(x => x.PromotionCategoryName).NotNull();
            RuleFor(x => x.PromotionCategoryName)
                .Must((promotionCategoryName) =>
                {
                    var isSuccessfulParsing = Enum.TryParse<PromotionCategories>(promotionCategoryName, out var promotionCategory);
                    return isSuccessfulParsing;
                });

            RuleFor(x => x.ElementId)
                .MustAsync( async (x, elementId, cancellation) =>
                {
                    var isSuccessfulParsing = Enum.TryParse<PromotionCategories>(x.PromotionCategoryName, out var promotionCategory);
                    if (!isSuccessfulParsing)
                    {
                        return false;
                    }

                    if (promotionCategory == PromotionCategories.ProductCategoryPromotion)
                    {
                        var doesCategoryExist = await categoryRepository.CheckIfExists(elementId);
                        return doesCategoryExist.IsSuccessful;
                    }

                    if (promotionCategory == PromotionCategories.ProductPromotion)
                    {
                        var existingProduct = await productRepository.GetProduct(elementId);
                        return existingProduct.IsSuccessful;
                    }

                    return true;
                });

            RuleFor(x => x.EndDate)
                .Must((x, endDate) =>
                {
                    return endDate > x.StartDate;
                })
                .When(x => x.EndDate.HasValue);
        }
    }
}
