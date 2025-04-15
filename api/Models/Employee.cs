using Microsoft.EntityFrameworkCore;
using System;

namespace api.Models
{
    [Index(nameof(EmployeeNumber), IsUnique = true)]
    public class Employee
    {
        public int Id { get; set; }
        public bool Active {  get; set; }
        public required string EmployeeNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public required string Title { get; set; }
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee> Reports { get; set; } = new List<Employee>();
        public HomeAddress? HomeAddress { get; set; }
        public string? Notes { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
