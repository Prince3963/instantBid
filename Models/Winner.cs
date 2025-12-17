using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace instantBid.Models
{
    public class Winner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Keys (existing tables)
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public decimal WinningAmount { get; set; }
        public DateTime AnnouncedAt { get; set; } = DateTime.Now;

        // Navigation
        public Auction? Auction { get; set; }
        public User? User { get; set; }
    }
}
