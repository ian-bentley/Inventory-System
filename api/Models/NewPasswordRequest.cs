namespace api.Models
{
    public class NewPasswordRequest
    {
        public required string UserId { get; set; }
        public required string NewPassword { get; set; }
    }
}
