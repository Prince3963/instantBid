namespace instantBid.DTOs
{
    public class CurrentBidDTO
    {
        public int AuctionId { get; set; }
        public string? AuctionItemName { get; set; }
        public int? EndingBid { get; set; }
    }
}
