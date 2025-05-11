namespace api.DTO
{
    public class NewPasswordRequest
    {
        public required string UserId { get; set; }
        public required string NewPassword { get; set; }
    }
}
