using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace UpskillStore.TableStorage.Models
{
    public class Category : TableEntity
    {
        public Category()
        {

        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = nameof(Category);
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
