using Microsoft.JSInterop;
using System.Text.Json;
using EcorpUI.Models;
using EcorpUI.Extensions;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcorpUI.Services
{
    public class ItemService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;
        private readonly IJSRuntime JS;
        public ItemService(APIService apiService, IConfiguration configuration, IJSRuntime jS)
        {
            this.apiService = apiService;
            this.configuration = configuration;
            JS = jS;
        }

        public async Task<IEnumerable<Item>> GetItemListForDashboard()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Item/GetItemsListForDashboard";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Item>?>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetItemsListForShopPage()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Item/GetItemsListForShopPage";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Item>?>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task<Item> GetItemDetail(int? id)
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Item/GetItemDetail/{id}" ;
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<Item>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteItem(Item item)
        {
            if (item.itemId > 0)
            {
                bool confirmResult = await JS.InvokeAsync<bool>("confirm", "Are you sure you delete the Item?");
                if (confirmResult)
                {
                    var requestUrl = configuration["APIBaseUrl"] + "Item/DeleteItem/" + item.itemId;
                    bool isSuccess = await apiService.DeleteAsync(requestUrl);
                    if (isSuccess)
                    {
                        JS.ToastrSuccess("Item Deleted Successfully !!!");
                    }
                    else
                    {
                        JS.ToastrError("Something Went Wrong !!!");
                    }

                }
            }
        }

        public async Task<ResponseModel> AddEditBlog(Item item)
        {
            var requestUrl = configuration["APIBaseUrl"] + "item/EditItem";

            if (item.itemId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "item/AddItem";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    itemId = item.itemId,
                    itemName = item.itemName,
                    itemDescription = item.itemDescription,
                    itemQuantity = item.itemQuantity,
                    itemRate = item.itemRate,
                    userId = item.userId,
                    itemImageList = item.itemImageList,
                }),
                Encoding.UTF8,
                "application/json");

            using var responseStream = await apiService.PostAsync(requestUrl, jsonContent);

            if (responseStream.Length > 0)
            {
                ResponseModel responseModel = await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);
                return responseModel;
            }
            else
            {
                JS.ToastrSuccess("Something went wrong !!!");
                return null;
            }

        }
    }
}
