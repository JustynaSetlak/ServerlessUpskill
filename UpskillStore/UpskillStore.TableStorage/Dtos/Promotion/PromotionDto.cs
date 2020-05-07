using System;

namespace UpskillStore.TableStorage.Dtos.Promotion
{
    public class PromotionDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PromotionCategoryName { get; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int Percentage { get; set; }

        public bool OnlyForVip { get; set; }

        public string RequiredCode { get; set; }

        public string ElementId { get; set; }
    }
}
