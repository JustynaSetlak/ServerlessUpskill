using UpskillStore.EventPublisher.EventHandlers;
using UpskillStore.Promotion.Enums;

namespace UpskillStore.Promotion.Events.ProductPromotion
{
    public class ProductPromotionExpired : IEvent
    {
        public ProductPromotionExpired(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
