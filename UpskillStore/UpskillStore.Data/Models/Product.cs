using System;

namespace UpskillStore.Data.Models
{
    public class Product
    {
        public Product(string name, string description, string categoryId)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            DateOfCreation = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; }

        public DateTime DateOfCreation { get; }
    }
}
