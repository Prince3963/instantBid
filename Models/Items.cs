using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace instantBid.Models
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string? ItemName {  get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemImage { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }


        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }


        public ICollection<Auction>? Auctions { get; set; }


    }
}
