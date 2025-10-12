using instantBid.Model;

namespace instantBid.DTOs
{
    public class RegistrationDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfileImage { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }


        //public Role? Role { get; set; }
    }
}
