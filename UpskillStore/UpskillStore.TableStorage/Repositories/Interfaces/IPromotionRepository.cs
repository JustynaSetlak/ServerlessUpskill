using System.Collections.Generic;
using System.Threading.Tasks;
using UpskillStore.TableStorage.Dtos.Promotion;
using UpskillStore.Utils.Result;

namespace UpskillStore.TableStorage.Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        Task<DataResult<CreateNewPromotionCommandResult>> Create(CreateNewPromotionCommand createNewPromotionCommand);
        Task<List<PromotionDto>> GetPromotions();
        Task<DataResult<PromotionDto>> GetById(string id, string partitionKey);
        Task<Result> Remove(string id, string partitionKey);
        Task<Result> ToggleActivePromotion(string id, string partitionKey);
        Task<List<PromotionDto>> GetActivePromotionByElementId(string promotionCategoryName, string elementId, bool onlyForVip);
    }
}