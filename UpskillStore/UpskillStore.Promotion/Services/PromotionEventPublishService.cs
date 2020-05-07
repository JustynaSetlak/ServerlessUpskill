using System;
using System.Threading.Tasks;
using UpskillStore.EventPublisher.EventHandlers;
using UpskillStore.Promotion.Enums;
using UpskillStore.Promotion.Events.CategoryPromotion;
using UpskillStore.Promotion.Events.OrderPromotion;
using UpskillStore.Promotion.Events.ProductPromotion;

namespace UpskillStore.Promotion.Services
{
    public class PromotionEventPublishService : IPromotionEventPublishService
    {
        private readonly IEventPublisher _eventPublisher;

        public PromotionEventPublishService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task PublishNewPromotionAppliedEvent(string id, string promotionCategoryName)
        {
            Enum.TryParse<PromotionCategories>(promotionCategoryName, out var promotionCategory);

            switch (promotionCategory)
            {
                case PromotionCategories.ProductCategoryPromotion:
                    await _eventPublisher.PublishEvent(new NewProductCategoryPromotionApplied(id));
                    break;
                case PromotionCategories.ProductPromotion:
                    await _eventPublisher.PublishEvent(new NewProductPromotionApplied(id));
                    break;
                case PromotionCategories.OrderPromotion:
                    await _eventPublisher.PublishEvent(new NewOrderPromotionApplied(id));
                    break;
            }
        }

        public async Task PublishPromotionExpiredEvent(string id, string promotionCategoryName)
        {
            Enum.TryParse<PromotionCategories>(promotionCategoryName, out var promotionCategory);

            switch (promotionCategory)
            {
                case PromotionCategories.ProductCategoryPromotion:
                    await _eventPublisher.PublishEvent(new ProductCategoryPromotionExpired(id));
                    break;
                case PromotionCategories.ProductPromotion:
                    await _eventPublisher.PublishEvent(new ProductPromotionExpired(id));
                    break;
                case PromotionCategories.OrderPromotion:
                    await _eventPublisher.PublishEvent(new OrderPromotionExpired(id));
                    break;
            }
        }
    }
}
