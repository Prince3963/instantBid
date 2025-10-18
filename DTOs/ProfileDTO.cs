namespace instantBid.DTOs
{
    public class ProfileDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfileImage { get; set; }
        //public string? ProfileImageURL { get;set; }
        public decimal? AccountBalance { get; set; }
        public string? Password { get; set; }
    }
}
