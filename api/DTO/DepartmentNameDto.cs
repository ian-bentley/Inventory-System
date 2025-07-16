namespace api.DTO
{
    public class DepartmentNameDto
    {
        public int Id { get; }
        
        public string Name { get;  }

        public DepartmentNameDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
