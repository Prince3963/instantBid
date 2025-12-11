namespace instantBid.DTOs
{
    public class BidHistoryDTO
    {
        //Auction Informatiion
        public int AuctionId { get; set; }
        public string? AuctionItemName { get; set; }
        public int? CurrentBid { get; set; }

        //User Infromation
        public int UserId { get; set; }
        public string? Name { get; set; }

        //Bid Information
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
    }
}
