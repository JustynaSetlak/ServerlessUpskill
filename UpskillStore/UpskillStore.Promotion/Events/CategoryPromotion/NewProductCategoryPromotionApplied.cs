using UpskillStore.EventPublisher.EventHandlers;

namespace UpskillStore.Promotion.Events.CategoryPromotion
{
    public class NewProductCategoryPromotionApplied : IEvent
    {
        public NewProductCategoryPromotionApplied(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
