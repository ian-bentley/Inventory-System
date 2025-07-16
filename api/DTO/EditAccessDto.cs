namespace api.DTO
{
    public class EditAccessDto
    {
        public required string UserId { get; set; }
        public bool ViewInventory { get; set; }
        public bool EditInventory { get; set; }
        public bool ViewEmployees { get; set; }
        public bool EditEmployees { get; set; }
        public bool ViewSecurity { get; set; }
        public bool EditSecurity { get; set; }
    }
}
