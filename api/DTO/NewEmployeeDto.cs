using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class NewEmployeeDto
    {
        [Required(ErrorMessage = "The active state of the employee is required.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "The employee number is required for identifying the employee.")]
        [StringLength(4, ErrorMessage = "The employee number must be four characters to comply with formatting policy.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "The employee number must be all numbers and not start with zero to comply with formatting policy.")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "The first name of the employee is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name of the employee is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The department id for the employee is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The job title of the employee is required")]
        public string Title { get; set; }

        public Guid? ManagerId { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "The first street line of the address is required.")]
        public string AddressStreet1 { get; }

        public string? AddressStreet2 { get; set; }

        [Required(ErrorMessage = "The city of the address is required.")]
        public string AddressCity { get; set; }

        [Required(ErrorMessage = "The state id of the address is required.")]
        public int UsStateId { get; set; }

        [Required(ErrorMessage = "The zip code of the address is required.")]
        public string AddressZip { get; set; }

        public NewEmployeeDto(
            bool active,
            string employeeNumber,
            string firstName,
            string lastName,
            int departmentId,
            string title,
            string addressStreet1,
            string addressCity,
            int usStateId,
            string addressZip)
        {
            Active = active;
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            DepartmentId = departmentId;
            Title = title;
            AddressStreet1 = addressStreet1;
            AddressCity = addressCity;
            UsStateId = usStateId;
            AddressZip = addressZip;
        }
    }
}
