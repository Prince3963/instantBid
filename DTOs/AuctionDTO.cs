namespace instantBid.DTOs
{
    public class AuctionDTO
    {
        public string? AuctionItemName { get; set; }
        public TimeOnly? AuctionStartTime { get; set; }
        public TimeOnly? AuctionEndTime { get; set; }
        public int? StartingBid { get; set; }
        public int? EndingBid { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }

    }
}
