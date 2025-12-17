using instantBid.DBContext;
using instantBid.DTOs;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace instantBid.Repositories.Implementations
{
    public class AuctionRepo : IAuctionRepoInterface
    {
        private readonly AppDbContext dbContext;
        public AuctionRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Auction?> AnnounceWinner(int auctionId)
        {
            var result = await dbContext.Auctions
                .Include(a => a.User)
                .Include(a => a.WinnerUser)
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.AuctionId == auctionId);
            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<Auction>> GetAllAuctions()
        {
            var result = await dbContext.Auctions
                .Include(u => u.User)
                .Include(i => i.Items)
                .ToListAsync();

            return result;
        }

        public async Task<Auction?> GetAuctionById(int id)
        {
            var result = await dbContext.Auctions
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.AuctionId == id);

            return result;
        }

        public async Task<Auction?> getAuctionByIDAndCurrentBid(int id)
        {
            var result = await dbContext.Auctions.FirstOrDefaultAsync(e => e.AuctionId == id);
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

        public async Task<List<Auction>> searchAuction(string auction)
        {
            if (string.IsNullOrWhiteSpace(auction))
            {
                return new List<Auction>();
            }

            auction = auction.ToLower();

            var result = await dbContext.Auctions
                .Include(i => i.Items)
                .Where(a => a.AuctionItemName != null && a.AuctionItemName!.ToLower().Contains(auction) ||
                            a.Items != null && a.Items.ItemName != null && a.Items!.ItemName!.ToLower().Contains(auction))
                .ToListAsync();

            return result;
        }

        public async Task updateAuctionStatus(Auction auction)
        {
            var result = dbContext.Auctions.Update(auction);
            await dbContext.SaveChangesAsync();

        }
    }
}
