using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(SerialNumber), IsUnique = true)]
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "The active state of the item is required.")]
        public bool? Active { get; set; }

        [Required(ErrorMessage = "The serial number of an item is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "The item type is of an item is required.")]
        public int? ItemTypeId { get; set; }

        public ItemType? ItemType { get; set; }

        [Required(ErrorMessage = "The model name of an item is required.")]
        public string Model { get; set; }

        public Guid? AssignedToId { get; set; }

        public Employee? AssignedTo { get; set; }

        public string? Notes { get; set; }

        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
