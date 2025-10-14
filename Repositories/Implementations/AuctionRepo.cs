using instantBid.DBContext;
using instantBid.Models;
using instantBid.Repositories.Interfaces;

namespace instantBid.Repositories.Implementations
{
    public class AuctionRepo : IAuctionRepoInterface
    {
        private readonly AppDbContext dbContext;
        public AuctionRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Auction> insertAuction(Auction auction)
        {
            try
            {
                await dbContext.Auctions.AddAsync(auction);
                await dbContext.SaveChangesAsync();

                return auction;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Console.WriteLine("Aucton Repo complete ");
            }
        }
    }
}
