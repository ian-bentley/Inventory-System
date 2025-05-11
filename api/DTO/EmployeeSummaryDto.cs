namespace api.DTO
{
    public class EmployeeSummaryDto
    {
        public Guid Id { get; }

        public bool Active { get; }

        public string EmployeeNumber { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public EmployeeSummaryDto(Guid id, bool active, string employeeNumber, string firstName, string lastName)
        {
            Id = id;
            Active = active;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
