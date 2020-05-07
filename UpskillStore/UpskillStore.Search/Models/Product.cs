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

        public string CategoryId { get; set; }

        public string Category { get; set; }

        public double OriginalPrice { get; set; }

        public PromotionDetails VipPromotionDetails { get; set; }

        public PromotionDetails CustomerPromotionDetails { get; set; }
    }
}
