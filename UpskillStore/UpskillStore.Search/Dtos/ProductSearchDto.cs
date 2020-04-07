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

        public string CategoryName { get; set; }
    }
}
