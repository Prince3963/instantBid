namespace instantBid.HelperServices
{
    public class ServiceResponses<G>
    {
        public G? data { get; set; }
        public string? message {  get; set; }
        public bool? status { get; set; }
    }
}
