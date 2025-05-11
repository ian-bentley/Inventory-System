using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ItemEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Item id for an item event is required.")]
        public Guid ItemId { get; set; }

        public Item? Item { get; set; }

        [Required(ErrorMessage = "Employee id for an item event is required.")]
        public Guid EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Event type id for an item event is required.")]
        public int? EventTypeId { get; set; }

        public EventType? EventType { get; set; }

        [Required(ErrorMessage = "A date for an item event is required.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "A reason for an item event is required.")]
        public string Reason { get; set; }
    }
}
