namespace instantBid.DTOs
{
    public class ItemsDTO
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public IFormFile? ItemImage { get; set; }
        public string? ItemImageURL {  get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }


        public int UserId { get; set; }
    }
}
