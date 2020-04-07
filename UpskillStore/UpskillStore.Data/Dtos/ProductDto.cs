namespace UpskillStore.Data.Dtos
{
    public class ProductDto
    {
        public ProductDto(string id, string name, string description, string categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }
    }
}
