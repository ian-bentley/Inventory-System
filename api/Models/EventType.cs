using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class EventType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
