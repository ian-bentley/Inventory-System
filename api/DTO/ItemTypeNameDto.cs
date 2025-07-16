namespace api.DTO
{
    public class ItemTypeNameDto
    {
        public int Id { get; }
        
        public string Name { get; }

        public ItemTypeNameDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
