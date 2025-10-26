using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IItemRepoInterface
    {
        Task<Items> addItem(Items item);
        Task<List<Items>> getAllItems();
    }
}
