using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using UpskillStore.Promotion.Services;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.Promotion.Functions.TimeTrigger
{
    public class UpdatePromotionsValidity
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IPromotionEventPublishService _promotionEventPublishService;

        public UpdatePromotionsValidity(IPromotionRepository promotionRepository, IPromotionEventPublishService promotionEventPublishService)
        {
            _promotionRepository = promotionRepository;
            _promotionEventPublishService = promotionEventPublishService;
        }

        [FunctionName(nameof(UpdatePromotionsValidity))]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer)
        {
            var validPromotions = await _promotionRepository.GetPromotions();

            foreach (var definedPromotion in validPromotions)
            {
                if (definedPromotion.IsActive 
                    && definedPromotion.EndDate.HasValue 
                    && definedPromotion.EndDate < DateTime.UtcNow.Date)
                {
                    await _promotionRepository.ToggleActivePromotion(definedPromotion.Id, definedPromotion.PromotionCategoryName);
                    await _promotionEventPublishService.PublishPromotionExpiredEvent(definedPromotion.Id, definedPromotion.PromotionCategoryName);
                }
                else if (!definedPromotion.IsActive
                    && definedPromotion.StartDate <= DateTime.UtcNow.Date
                    && (!definedPromotion.EndDate.HasValue
                        || definedPromotion.EndDate.HasValue && definedPromotion.EndDate >= DateTime.UtcNow.Date))
                {
                    await _promotionRepository.ToggleActivePromotion(definedPromotion.Id, definedPromotion.PromotionCategoryName);
                    await _promotionEventPublishService.PublishNewPromotionAppliedEvent(definedPromotion.Id, definedPromotion.PromotionCategoryName);
                }

            }
        }
    }
}
