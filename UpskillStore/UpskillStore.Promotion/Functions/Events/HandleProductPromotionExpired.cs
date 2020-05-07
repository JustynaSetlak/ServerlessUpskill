// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using UpskillStore.TableStorage.Repositories.Interfaces;
using System.Threading.Tasks;
using UpskillStore.Search.Services;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.Events.ProductPromotion;
using UpskillStore.Data.Repositories;
using System.Linq;
using UpskillStore.Search.Dtos;
using UpskillStore.TableStorage.Dtos.Promotion;

namespace UpskillStore.Promotion.Functions.Events
{
    public class HandleProductPromotionExpired
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IProductSearchService _productSearchService;

        public HandleProductPromotionExpired(IPromotionRepository promotionRepository, IProductSearchService productSearchService)
        {
            _promotionRepository = promotionRepository;
            _productSearchService = productSearchService;
        }

        [FunctionName(nameof(HandleProductPromotionExpired))]
        public async Task Run([EventGridTrigger]ProductPromotionExpired productPromotionExpired)
        {
            var promotionResult = await _promotionRepository.GetById(productPromotionExpired.Id, PromotionCategories.ProductPromotion.ToString());
            
            if (!promotionResult.IsSuccessful || promotionResult.Value.IsActive)
            {
                return;
            }

            var searchedProduct = await _productSearchService.GetById(promotionResult.Value.ElementId);

            if (promotionResult.Value.OnlyForVip)
            {
                searchedProduct.VipPromotionDetails = await this.GetMostBeneficialPromotionDetails(searchedProduct.Id, searchedProduct.OriginalPrice, searchedProduct.CategoryId, true);
            }

            searchedProduct.VipPromotionDetails = await this.GetMostBeneficialPromotionDetails(searchedProduct.Id, searchedProduct.OriginalPrice, searchedProduct.CategoryId, true);
            searchedProduct.CustomerPromotionDetails = await this.GetMostBeneficialPromotionDetails(searchedProduct.Id, searchedProduct.OriginalPrice, searchedProduct.CategoryId, false);

            await _productSearchService.MergeOrUpload(searchedProduct);
        }

        private async Task<PromotionDetailsDto> GetMostBeneficialPromotionDetails(string productId, double originalPrice, string categoryId, bool forVip)
        {
            var validPromotion = await GetMostBeneficialPromotion(productId, categoryId, forVip);

            var promotionPrice = originalPrice * (1 - validPromotion.Percentage / 100);
            return new PromotionDetailsDto(validPromotion.Percentage, validPromotion.Name, promotionPrice);
        }

        private async Task<PromotionDto> GetMostBeneficialPromotion(string elementId, string productCategoryId, bool forVipPromotion)
        {
            var productPromotions = await _promotionRepository.GetActivePromotionByElementId(
                    PromotionCategories.ProductPromotion.ToString(),
                    elementId,
                    forVipPromotion);

            var productCategoryPromotions = await _promotionRepository.GetActivePromotionByElementId(
                PromotionCategories.ProductCategoryPromotion.ToString(),
                productCategoryId,
                forVipPromotion);

            var mostBeneficialProductPromotion = productPromotions.OrderByDescending(x => x.Percentage).FirstOrDefault();
            var mostBeneficialCategoryPromotion = productCategoryPromotions.OrderByDescending(x => x.Percentage).FirstOrDefault();

            if(mostBeneficialProductPromotion == null && mostBeneficialCategoryPromotion == null)
            {
                return null;
            }

            if (mostBeneficialCategoryPromotion == null)
            {
                return mostBeneficialProductPromotion;
            }

            if (mostBeneficialProductPromotion == null)
            {
                return mostBeneficialCategoryPromotion;
            }

            return mostBeneficialProductPromotion.Percentage >= mostBeneficialCategoryPromotion.Percentage
                    ? mostBeneficialProductPromotion
                    : mostBeneficialCategoryPromotion;
        }
    }
}
