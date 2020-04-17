using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace UpskillStore.TableStorage.Models
{
    public class Promotion : TableEntity
    {
        public Promotion(string promotionCategoryName)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = promotionCategoryName;
            PromotionCategoryName = promotionCategoryName;
        }

        public string PromotionCategoryName { get; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public int Percentage { get; set; }

        public bool OnlyForVip { get; set; }

        public string ElementId { get; set; }
    }
}
