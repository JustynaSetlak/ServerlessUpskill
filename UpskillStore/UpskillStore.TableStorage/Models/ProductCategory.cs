using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace UpskillStore.TableStorage.Models
{
    public class ProductCategory : TableEntity
    {
        public ProductCategory()
        {
        }

        public ProductCategory(string name, string description)
        {
            Name = name;
            Description = description;
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = nameof(ProductCategory);
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
