using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ItemEvent
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int EventTypeId { get; set; }
        public EventType? EventType { get; set; }
        public DateTime DateTime { get; set; }
        public required string Reason { get; set; }
    }
}
