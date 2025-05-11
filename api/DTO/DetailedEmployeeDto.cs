using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class DetailedEmployeeDto
    {
        public Guid Id { get; }

        public bool Active { get; }

        public string EmployeeNumber { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string DepartmentName { get; }

        public string Title { get; }

        public string? ManagerFullName { get; init; }

        public string? Notes { get; init; }

        public string AddressStreet1 { get; }

        public string? AddressStreet2 { get; init; }

        public string AddressCity { get; }

        public string UsStateInitials { get; }

        public string AddressZip { get; }


        public DetailedEmployeeDto(
            Guid id,
            bool active,
            string employeeNumber,
            string firstName,
            string lastName,
            string departmentName,
            string title,
            string addressStreet1,
            string addressCity,
            string addressZip,
            string usStateInitials)
        {
            Id = id;
            Active = active;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            DepartmentName = departmentName;
            Title = title;
            AddressStreet1 = addressStreet1;
            AddressCity = addressCity;
            AddressZip = addressZip;
            UsStateInitials = usStateInitials;
        }
    }
}
