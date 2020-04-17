using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace UpskillStore.TableStorage.Models
{
    public class PromotionCategory : TableEntity
    {
        public PromotionCategory()
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = nameof(PromotionCategory);
        }

        public string Name { get; set; }

        public string Details { get; set; }
    }
}
