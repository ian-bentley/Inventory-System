using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ItemEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemId { get; set; }

        public Item? Item { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public int EventTypeId { get; set; }

        public EventType? EventType { get; set; }

        public DateTime DateTime { get; set; }

        public string Reason { get; set; }
    }
}
