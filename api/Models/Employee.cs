using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    [Index(nameof(EmployeeNumber), IsUnique = true)]
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "The active state of the employee is required.")]
        public bool? Active {  get; set; }
        
        [Required(ErrorMessage = "The employee number is required for identifying the employee.")]
        [StringLength(4, ErrorMessage = "The employee number must be four characters to comply with formatting policy.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "The employee number must be all numbers and not start with zero to comply with formatting policy.")]
        public string EmployeeNumber { get; set; }
        
        [Required(ErrorMessage = "The first name of the employee is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name of the employee is required.")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "The employee department id is required.")]
        public int? DepartmentId { get; set; }
        
        public Department? Department { get; set; }

        [Required(ErrorMessage = "The job title of the employee is required")]
        public string Title { get; set; }
        
        public Guid? ManagerId { get; set; }
        
        public Employee? Manager { get; set; }
        
        public ICollection<Employee> Reports { get; set; } = new List<Employee>();
        
        public string? Notes { get; set; }
        
        public ICollection<Item> Items { get; set; } = new List<Item>();
        
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
