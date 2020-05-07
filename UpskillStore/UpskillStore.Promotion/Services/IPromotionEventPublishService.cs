using System.Threading.Tasks;

namespace UpskillStore.Promotion.Services
{
    public interface IPromotionEventPublishService
    {
        Task PublishNewPromotionAppliedEvent(string id, string promotionCategoryName);
        Task PublishPromotionExpiredEvent(string id, string promotionCategoryName);
    }
}