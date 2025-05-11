namespace api.DTO
{
    public class EmployeeNameDto
    {
        public Guid Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public EmployeeNameDto(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
