namespace UpskillStore.TableStorage.Dtos
{
    public class CategoryDto
    {
        public CategoryDto(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
