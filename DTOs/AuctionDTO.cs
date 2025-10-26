namespace instantBid.DTOs
{
    public class AuctionDTO
    {

        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public IFormFile? ItemImage { get; set; }
        public string? ItemImageURL { get; set; }
        public string? AuctionItemName { get; set; }
        public DateTime? AuctionStartTime { get; set; }
        public DateTime? AuctionEndTime { get; set; }
        public int? StartingBid { get; set; }
        public int? CurrentBid { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }
    }
}
