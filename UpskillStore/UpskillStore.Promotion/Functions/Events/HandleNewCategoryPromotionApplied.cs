// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using System.Threading.Tasks;
using UpskillStore.Search.Services;
using UpskillStore.TableStorage.Repositories.Interfaces;
using UpskillStore.Search.Dtos;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.Events.CategoryPromotion;

namespace UpskillStore.Promotion.Functions.Events
{
    public class HandleNewCategoryPromotionApplied
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IProductSearchService _productSearchService;
        private readonly ICategoryRepository _categoryRepository;

        public HandleNewCategoryPromotionApplied(
            IPromotionRepository promotionRepository, 
            IProductSearchService productSearchService,
            ICategoryRepository categoryRepository)
        {
            _promotionRepository = promotionRepository;
            _productSearchService = productSearchService;
            _categoryRepository = categoryRepository;
        }

        [FunctionName(nameof(HandleNewCategoryPromotionApplied))]
        public async Task Run([EventGridTrigger]NewProductCategoryPromotionApplied newPromotionCreated)
        {
            var promotionResult = await _promotionRepository.GetById(newPromotionCreated.Id, PromotionCategories.ProductCategoryPromotion.ToString());

            if (!promotionResult.IsSuccessful)
            {
                return;
            }

            var existingCategoryResult = await _categoryRepository.GetById(promotionResult.Value.ElementId);

            if (!existingCategoryResult.IsSuccessful)
            {
                return;
            }

            var productSearchParams = new ProductSearchParamsDto(existingCategoryResult.Value.Name);

            var searchedProductList = await _productSearchService.Search(productSearchParams);

            foreach(var searchedProduct in searchedProductList)
            {
                var promotionPrice = searchedProduct.OriginalPrice * (1 - promotionResult.Value.Percentage / 100);

                if (!promotionResult.Value.OnlyForVip)
                {
                    searchedProduct.CustomerPromotionDetails = new PromotionDetailsDto(promotionResult.Value.Percentage, promotionResult.Value.Name, promotionPrice);
                }

                if (searchedProduct.VipPromotionDetails.PromotionPrice > promotionPrice)
                {
                    searchedProduct.VipPromotionDetails = new PromotionDetailsDto(promotionResult.Value.Percentage, promotionResult.Value.Name, promotionPrice);
                }

                await _productSearchService.MergeOrUpload(searchedProduct);
            }
        }
    }
}
