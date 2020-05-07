using UpskillStore.EventPublisher.EventHandlers;

namespace UpskillStore.Promotion.Events.ProductPromotion
{
    public class NewProductPromotionApplied : IEvent
    {
        public NewProductPromotionApplied(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
