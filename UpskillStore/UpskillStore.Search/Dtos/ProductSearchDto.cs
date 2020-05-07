namespace UpskillStore.Search.Dtos
{
    public class ProductSearchDto
    {
        public ProductSearchDto(string id, string name, string description, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryName = categoryName;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public double OriginalPrice { get; set; }

        public PromotionDetailsDto VipPromotionDetails { get; set; }

        public PromotionDetailsDto CustomerPromotionDetails { get; set; }
    }
}
