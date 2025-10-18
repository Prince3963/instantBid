using instantBid.Model;

namespace instantBid.DTOs
{
    public class RegistrationDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImageURL { get; set; }
        public string? Address { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }


        //public Role? Role { get; set; }
    }
}
