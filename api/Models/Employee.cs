using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    [Index(nameof(EmployeeNumber), IsUnique = true)]
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active {  get; set; }
        
        [StringLength(4)]
        [RegularExpression(@"^[1-9][0-9]*$")]
        public required string EmployeeNumber { get; set; }
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int DepartmentId { get; set; }
        
        public Department? Department { get; set; }

        public required string Title { get; set; }
        
        public Guid? ManagerId { get; set; }
        
        public Employee? Manager { get; set; }
        
        public ICollection<Employee> Reports { get; set; } = new List<Employee>();
        
        public string? Notes { get; set; }

        public EmployeeAddressLink? EmployeeAddressLink { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
        
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
