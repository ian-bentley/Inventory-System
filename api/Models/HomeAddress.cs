namespace api.Models
{
    public class HomeAddress
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public required Employee Employee { get; set; }
        public required string Street1 { get; set; }
        public string? Street2 { get; set; }
        public required string City { get; set; }
        public int UsStateId { get; set; }
        public required UsState UsState { get; set; }
        public required string Zip {  get; set; }
    }
}
