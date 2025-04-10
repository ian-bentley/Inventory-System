using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Initials), IsUnique = true)]
    public class UsState
    {
        public int Id { get; set; }
        public required string Initials { get; set; }
        //public ICollection<HomeAddress> HomeAddresses { get; set; } = new List<HomeAddress>();
    }
}
