namespace api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public required string SnSt { get; set; }
        public int ItemTypeId { get; set; }
        public required string Model { get; set; }
        public string? Notes { get; set; }
    }
}
