using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Initials), IsUnique = true)]
    public class UsState
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The intials of the state is required.")]
        public required string Initials { get; set; }
    }
}
