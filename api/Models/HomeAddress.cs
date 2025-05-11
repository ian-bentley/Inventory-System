using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class HomeAddress
    {
        public Guid Id { get; set; }

        public required string Street1 { get; set; }

        public string? Street2 { get; set; }

        public required string City { get; set; }

        public int UsStateId { get; set; }

        public UsState? UsState { get; set; }

        public required string Zip { get; set; }
    }
}
