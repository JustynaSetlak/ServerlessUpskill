namespace UpskillStore.Search.Models
{
    public class Product : ISearchable
    {
        public Product(string id, string name, string description, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = categoryName;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
