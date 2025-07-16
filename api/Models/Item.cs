using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(SerialNumber), IsUnique = true)]
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active { get; set; }

        public string SerialNumber { get; set; }

        public int ItemTypeId { get; set; }

        public ItemType? ItemType { get; set; }

        public string Model { get; set; }

        public Guid? AssignedToId { get; set; }

        public Employee? AssignedTo { get; set; }

        public string? Notes { get; set; }

        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
