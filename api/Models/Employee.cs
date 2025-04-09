namespace api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required bool Active { get; set; }
        public required string EmployeeNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int DepartmentId { get; set; }
        public required string Title { get; set; }
        public required int ManagerId { get; set; }
        public required int HomeAddressId { get; set; }

    }
}
