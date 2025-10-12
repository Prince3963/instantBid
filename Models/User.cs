using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using instantBid.Model;

namespace instantBid.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? ProfileImage { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

        //Foreign Key
        public int RoleId { get; set; }
        
        //Navigate Role
        public Role? Role { get; set; }
    }
}
