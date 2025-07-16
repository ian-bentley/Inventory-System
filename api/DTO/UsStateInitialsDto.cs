namespace api.DTO
{
    public class UsStateInitialsDto
    {
        public int Id { get; }

        public string Initials { get; }

        public UsStateInitialsDto(int id, string initials)
        {
            Id = id;
            Initials = initials;
        }
    }
}
