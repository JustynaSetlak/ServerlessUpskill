using System;
using System.Collections.Generic;
using System.Text;

namespace UpskillStore.Search.Dtos
{
    public class PromotionDetailsDto
    {
        public PromotionDetailsDto(int percentageDiscount, string name, double promotionPrice)
        {
            PercentageDiscount = percentageDiscount;
            Name = name;
            PromotionPrice = promotionPrice;
        }

        public int PercentageDiscount { get; set; }

        public string Name { get; set; }

        public double PromotionPrice { get; set; }
    }
}
