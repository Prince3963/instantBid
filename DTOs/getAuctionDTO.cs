namespace instantBid.DTOs
{
    public class getAuctionDTO
    {
        public string? AuctionItemName { get; set; }
        public DateTime? AuctionStartTime { get; set; }
        public DateTime? AuctionEndTime { get; set; }
        public int? StartingBid { get; set; }
        public int? EndingBid { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
