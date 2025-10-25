using instantBid.DBContext;
using instantBid.DTOs;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace instantBid.Repositories.Implementations
{
    public class AuctionRepo : IAuctionRepoInterface
    {
        private readonly AppDbContext dbContext;
        public AuctionRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Auction>> GetAllAuctions()
        {
            var result = await dbContext.Auctions
                .ToListAsync();

            return result;
        }

        public async Task<Auction?> GetAuctionById(int id)
        {
            var result = await dbContext.Auctions
                .Include (a => a.User)
                .FirstOrDefaultAsync(a => a.UserId == id);

            return result;
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
