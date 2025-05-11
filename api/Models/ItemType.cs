using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class ItemType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name of the item type is required.")]
        public string Name { get; set; }
        
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
