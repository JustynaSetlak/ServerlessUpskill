using System;

namespace UpskillStore.TableStorage.Dtos.Promotion
{
    public class CreateNewPromotionCommand
    {
        public string Id { get; set; }

        public string PromotionCategoryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public int Percentage { get; set; }

        public bool OnlyForVip { get; set; }

        public string RequiredCode { get; set; }

        public string ElementId { get; set; }

    }
}
