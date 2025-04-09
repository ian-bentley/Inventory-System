namespace api.Models
{
    public class HomeAddress
    {
        public int Id { get; set; }
        public required string Street1 { get; set; }
        public string? Street2 { get; set; }
        public required string City { get; set; }
        public required int StateId { get; set; }
        public required string Zip { get; set; }
    }
}
