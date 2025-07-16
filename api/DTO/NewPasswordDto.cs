namespace api.DTO
{
    public class NewPasswordDto
    {
        public required string UserId { get; set; }
        public required string NewPassword { get; set; }
    }
}
