using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(SerialNumber), IsUnique = true)]
    public class Item
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public required string SerialNumber { get; set; }
        public int ItemTypeId { get; set; }
        public ItemType? ItemType { get; set; }
        public required string Model { get; set; }
        public int? CurrentEventTypeId { get; set; }
        public EventType? CurrentEventType { get; set; }
        public int? AssignedToId { get; set; }
        public Employee? AssignedTo { get; set; }
        public string? Notes { get; set; }
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
