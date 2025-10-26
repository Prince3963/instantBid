using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;
using instantBid.Repositories.Implementations;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Interfaces;

namespace instantBid.Services.Implementstions
{
    public class ItemServices : IItemServiceInterface
    {
        private readonly IItemRepoInterface itemRepo;
        private readonly CloudinaryService cloudinaryService;
        public ItemServices(CloudinaryService cloudinaryService, IItemRepoInterface itemRepo)
        {
            this.itemRepo = itemRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<ServiceResponses<string>> addItem(ItemsDTO itemDTO)
        {
            var response = new ServiceResponses<string>();
            var imageURL = await cloudinaryService.uploadImages(itemDTO.ItemImage);
            Console.WriteLine("Image : " + imageURL);
            try
            {
                var newItems = new Items
                {
                    ItemName = itemDTO.ItemName,
                    ItemDescription = itemDTO.ItemDescription,
                    ItemImage = imageURL,
                    CreatedAt = itemDTO.CreatedAt ?? DateTime.Now,
                    Status = itemDTO.Status ?? true,
                    UserId = itemDTO.UserId,
                };

                await itemRepo.addItem(newItems);

                response.data = "1";
                response.message = "New Item Added Successfully ";
                response.status = true;

                return response;

            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = ("Check Item Servicee" + ex);
                response.status = false;

                return response;
            }
        }

        public async Task<ServiceResponses<List<ItemsDTO>>> getAllItem()
        {
            var response = new ServiceResponses<List<ItemsDTO>>();

            try
            {
                var newItems = await itemRepo.getAllItems();

                if (newItems == null)
                {
                    response.data = null;
                    response.message = "Item is null";
                    response.status = false;

                    return response;
                }

                var result = newItems.Select(i => new ItemsDTO
                {
                    ItemName = i.ItemName,
                    ItemDescription = i.ItemDescription,
                    CreatedAt=i.CreatedAt ?? DateTime.Now,
                    ItemImageURL = i.ItemImage,
                    Status = i.Status,
                    UserId = i.UserId,
                }).ToList();

                response.data = result;
                response.message = "Items fetched";
                response.status = true;
                
                return response;

            }catch(Exception e)
            {
                response.data = null;
                response.message = "Check your Item Service";
                response.status = false;

                return response;
            }
        }
    }
}
