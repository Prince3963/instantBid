using instantBid.DTOs;
using instantBid.HelperServices;

namespace instantBid.Services.Interfaces
{
    public interface IItemServiceInterface
    {
        Task<ServiceResponses<string>> addItem(ItemsDTO itemDTO);
        Task<ServiceResponses<List<ItemsDTO>>> getAllItem();
    }
}
