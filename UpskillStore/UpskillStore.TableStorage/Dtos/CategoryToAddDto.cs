namespace UpskillStore.TableStorage.Dtos
{
    public class CategoryToAddDto
    {
        public CategoryToAddDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; }
    }
}