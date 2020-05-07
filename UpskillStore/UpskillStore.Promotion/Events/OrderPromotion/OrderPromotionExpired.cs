using UpskillStore.EventPublisher.EventHandlers;

namespace UpskillStore.Promotion.Events.OrderPromotion
{
    public class OrderPromotionExpired : IEvent
    {
        public OrderPromotionExpired(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
