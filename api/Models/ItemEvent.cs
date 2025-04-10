using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ItemEvent
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        public int EmployeeId { get; set; }
        public required Employee Employee { get; set; }
        public int EventTypeId { get; set; }
        public required EventType EventType { get; set; }
        public DateTime DateTime { get; set; }
        public required string Reason { get; set; }
    }
}
