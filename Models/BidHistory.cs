using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace instantBid.Models
{
    public class BidHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BidId { get; set; }

        public int AuctionId { get; set; }
        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.Now;
    }
}
