using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace UpskillStore.TableStorage.Models
{
    public class Promotion : TableEntity
    {
        public Promotion()
        {
        }

        public Promotion(string promotionCategoryName, string identificator)
        {
            RowKey = identificator;
            PartitionKey = promotionCategoryName;
            PromotionCategoryName = promotionCategoryName;
        }

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
