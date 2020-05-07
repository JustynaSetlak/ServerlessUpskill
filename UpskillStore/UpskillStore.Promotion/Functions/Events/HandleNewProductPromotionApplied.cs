// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using System.Threading.Tasks;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.Events.ProductPromotion;
using UpskillStore.Search.Dtos;
using UpskillStore.Search.Services;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.Promotion.Functions.Events
{
    public class HandleNewProductPromotionApplied
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IProductSearchService _productSearchService;

        public HandleNewProductPromotionApplied(IPromotionRepository promotionRepository, IProductSearchService productSearchService)
        {
            _promotionRepository = promotionRepository;
            _productSearchService = productSearchService;
        }

        [FunctionName(nameof(HandleNewProductPromotionApplied))]
        public async Task Run([EventGridTrigger]NewProductPromotionApplied newPromotionCreated)
        {
            var promotionResult = await _promotionRepository.GetById(newPromotionCreated.Id, PromotionCategories.ProductPromotion.ToString());

            if (!promotionResult.IsSuccessful)
            {
                return;
            }

            var searchedProduct = await _productSearchService.GetById(promotionResult.Value.ElementId);

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
