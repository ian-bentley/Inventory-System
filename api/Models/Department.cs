using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A name for the department is required.")]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
