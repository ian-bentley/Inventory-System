using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class HomeAddress
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The first street line of the address is required.")]
        public string Street1 { get; set; }

        public string? Street2 { get; set; }

        [Required(ErrorMessage = "The city of the address is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "The state id of the address is required.")]
        public int? UsStateId { get; set; }

        public UsState? UsState { get; set; }

        [Required(ErrorMessage = "The zip code of the address is required.")]
        public string Zip { get; set; }
    }
}
