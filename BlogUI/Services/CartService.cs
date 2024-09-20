using Microsoft.JSInterop;
using System.Text.Json;
using EcorpUI.Models;
using EcorpUI.Extensions;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EcorpAPI.Models;

namespace EcorpUI.Services
{
    public class CartService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;
        private readonly IJSRuntime JS;
        public CartService(APIService apiService, IConfiguration configuration, IJSRuntime jS)
        {
            this.apiService = apiService;
            this.configuration = configuration;
            JS = jS;
        }

        public async Task<IEnumerable<CartItemModel>> GetCartItemsAsync(int? userId)
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Cart/GetCartItems?userId={userId}";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<CartItemModel>?>(responseStream);
            }
            else
            {
                return null;
            }
        }
       
        public async Task<decimal?> GetCartTotalAsync(int? userId)
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Cart/GetCartTotal?userId={userId}";
            var responseStream = await apiService.SendAsync(requestUrl, true);

            if (responseStream != null)
            {
                var result = await JsonSerializer.DeserializeAsync<decimal?>(responseStream);
                return result;
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> RemoveCart(CartItemModel cartItem)
        {
            bool confirmResult = await JS.InvokeAsync<bool>("confirm", "Are you sure you want to remove the item?");

            if (confirmResult)
            {
                var requestUrl = configuration["APIBaseUrl"] + $"Cart/RemoveFromCart?userId={cartItem.userId}&cartItemId={cartItem.cartItemId}";
                return await apiService.DeleteAsync(requestUrl); 
            }
            else
            {
                return false;
            }
        }

        public async Task<ResponseModel> AddToCart(CartItemModel cartItem)
        {
            var requestUrl = configuration["APIBaseUrl"] + "Cart/UpdateQuantity";

            if (cartItem.cartItemId == 0)
            {
                requestUrl = configuration["APIBaseUrl"] + "Cart/AddToCart";
            }
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    itemId = cartItem.itemId,
                    cartItemId = cartItem.cartItemId,
                    userId = cartItem.userId,
                    quantity = cartItem.quantity,
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

        public async Task<ResponseModel> CheckoutCart()
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Cart/CheckoutCart";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);
            }
            else
            {
                return new ResponseModel();
            }
        }

        public async Task<List<ConfirmedOrder>> GetItemSoldDetails()
        {
            var requestUrl = configuration["APIBaseUrl"] + $"Cart/GetSoldItemsDetail";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<List<ConfirmedOrder>>(responseStream);
            }
            else
            {
                return new List<ConfirmedOrder>();
            }
        }
    }
}
