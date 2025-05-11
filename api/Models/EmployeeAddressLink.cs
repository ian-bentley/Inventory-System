using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class EmployeeAddressLink
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required(ErrorMessage = "The employee id for an employee address link is required.")]
        public Guid EmployeeId { get; set; }
        
        public Employee? Employee { get; set; }
        
        [Required(ErrorMessage = "The address id for an employee address link is required.")]
        public Guid AddressId { get; set; }
        
        public HomeAddress? HomeAddress { get; set; }
    }
}
