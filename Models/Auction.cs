using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace instantBid.Models
{
    public class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AuctionId { get; set; }
        public string? AuctionItemName { get; set; }
        public TimeSpan? AuctionStartTime { get; set; }
        public TimeSpan? AuctionEndTime { get; set; }
        public int? StartingBid {  get; set; }
        public int? EndingBid { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId {  get; set; }
        public User? User { get; set; }
    }
}
