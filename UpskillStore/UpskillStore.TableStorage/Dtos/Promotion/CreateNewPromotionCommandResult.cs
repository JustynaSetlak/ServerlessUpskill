namespace UpskillStore.TableStorage.Dtos.Promotion
{
    public class CreateNewPromotionCommandResult
    {
        public CreateNewPromotionCommandResult(string id, string promotionCategoryName)
        {
            Id = id;
            PromotionCategoryName = promotionCategoryName;
        }

        public string Id { get; set; }

        public string PromotionCategoryName { get; set; }
    }
}
