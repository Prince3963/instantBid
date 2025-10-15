namespace instantBid.DTOs
{
    public class ProfileDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfileImage { get; set; }
        public decimal? AccountBalance { get; set; }
    }
}
