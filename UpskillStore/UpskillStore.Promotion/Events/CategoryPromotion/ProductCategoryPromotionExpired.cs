using UpskillStore.EventPublisher.EventHandlers;
using UpskillStore.Promotion.Enums;

namespace UpskillStore.Promotion.Events.CategoryPromotion
{
    public class ProductCategoryPromotionExpired : IEvent
    {
        public ProductCategoryPromotionExpired(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
