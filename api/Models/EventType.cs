using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class EventType
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "The name for an event type is required")]
        public string Name { get; set; }
        
        public ICollection<ItemEvent> ItemEvents { get; set; } = new List<ItemEvent>();
    }
}
