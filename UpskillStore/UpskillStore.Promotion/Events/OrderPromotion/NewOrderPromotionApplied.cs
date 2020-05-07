using UpskillStore.EventPublisher.EventHandlers;

namespace UpskillStore.Promotion.Events.OrderPromotion
{
    public class NewOrderPromotionApplied : IEvent
    {
        public NewOrderPromotionApplied(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
