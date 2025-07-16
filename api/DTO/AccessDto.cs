namespace api.DTO
{
    public class AccessDto
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool ViewInventory { get; set; }
        public bool EditInventory { get; set; }
        public bool ViewEmployees { get; set; }
        public bool EditEmployees { get; set; }
        public bool ViewSecurity { get; set; }
        public bool EditSecurity { get; set; }
    }
}
