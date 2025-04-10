using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class ItemType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
