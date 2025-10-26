using instantBid.DBContext;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace instantBid.Repositories.Implementations
{
    public class ItemRepo : IItemRepoInterface
    {
        private readonly AppDbContext dbContext;
        public ItemRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Items> addItem(Items item)
        {
            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<List<Items>> getAllItems()
        {
            return await dbContext.Items.ToListAsync();
        }
    }
}
