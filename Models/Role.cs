using instantBid.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace instantBid.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        //Navigation property
        public ICollection<User>? Users { get; set; }
    }
}
