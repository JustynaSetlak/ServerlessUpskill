using System;

namespace UpskillStore.Promotion.HttpRequests
{
    public class CreatePromotionHttpRequest
    {
        public string PromotionCategoryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Percentage { get; set; }

        public bool OnlyForVip { get; set; }

        public string RequiredCode { get; set; }

        public string ElementId { get; set; }
    }
}
