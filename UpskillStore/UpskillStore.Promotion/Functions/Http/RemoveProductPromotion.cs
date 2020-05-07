using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using UpskillStore.Common.Constants;
using UpskillStore.EventPublisher.EventHandlers;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.Events.ProductPromotion;
using UpskillStore.Promotion.HttpRequests;
using UpskillStore.Promotion.Services;
using UpskillStore.TableStorage.Repositories.Interfaces;

namespace UpskillStore.Promotion.Functions.Http
{
    public class RemoveProductPromotion
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IEventPublisher _eventPublisher;

        public RemoveProductPromotion(IPromotionRepository promotionRepository, IEventPublisher eventPublisher)
        {
            _promotionRepository = promotionRepository;
            _eventPublisher = eventPublisher;
        }

        [FunctionName(nameof(RemoveProductPromotion))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethodNames.DELETE)] RemoveProductPromotionHttpRequest removePromotionHttpRequest)
        {
            var result = await _promotionRepository.Remove(removePromotionHttpRequest.PromotionId, PromotionCategories.ProductPromotion.ToString());

            if (!result.IsSuccessful)
            {
                return new BadRequestResult();
            }

            await _eventPublisher.PublishEvent(new ProductPromotionExpired(removePromotionHttpRequest.PromotionId));

            return new NoContentResult();
        }
    }
}
