using Microsoft.JSInterop;
using System.Text.Json;
using EcorpUI.Models;
using EcorpUI.Extensions;
using System.Text;
using EcorpUI.Components.Pages.User;
using Microsoft.AspNetCore.Components.Forms;

namespace EcorpUI.Services
{
    public class AccountService
    {
        private readonly APIService apiService;
        private readonly IConfiguration configuration;
        private readonly IJSRuntime JS;

        public AccountService(APIService apiService, IConfiguration configuration, IJSRuntime jS)
        {
            this.apiService = apiService;
            this.configuration = configuration;
            this.JS = jS;
        }

        public async Task<byte[]> HandleImageUpload(IBrowserFile? uploadedFile)
        {
            if (uploadedFile != null)
            {
                const long maxFileSize = 1024 * 1024 * 2; // 2 MB limit

                if (uploadedFile.Size > maxFileSize)
                {
                    return new byte[0];
                }

                using (var memoryStream = new MemoryStream())
                {
                    await uploadedFile.OpenReadStream(maxFileSize).CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return new byte[0];
        }

        public async Task<IEnumerable<UserDetails>> GetUserList()
        {
            var requestUrl = configuration["APIBaseUrl"] + "Account/GetAllUser";
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<UserDetails>?>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDetails> GetSingleUserDetail(int id)
        {
            var requestUrl = configuration["APIBaseUrl"] + "Account/GetSingleUser/" + id;
            var responseStream = await apiService.SendAsync(requestUrl, true);
            if (responseStream != null)
            {
                return await JsonSerializer.DeserializeAsync<UserDetails?>(responseStream);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeactivateUser(int? user)
        {
            if (user > 0)
            {
                bool confirmResult = await JS.InvokeAsync<bool>("confirm", "Are you sure you deactivate the User?");
                if (confirmResult)
                {
                    var requestUrl = configuration["APIBaseUrl"] + "account/deactivateUser/" + user ;
                    var responseStream = await apiService.SendAsync(requestUrl,true);
                    if (responseStream != null)
                    {
                        var result = await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);
                        if (result.isSuccess == true)
                        {
                            JS.ToastrSuccess("User Deactivated Successfully !!!");
                            return true;
                        }
                        else
                        {
                            JS.ToastrError("Something Went Wrong !!!");
                        }
                    }
                    else
                    {
                    }
                }
            }
            return false;
        }

        public async Task<bool> ReactivateUser(int? userId)
        {
            if (userId > 0)
            {
                var requestUrl = configuration["APIBaseUrl"] + "account/reactivateUser/" + userId;
                var responseStream = await apiService.SendAsync(requestUrl, true);
                if(responseStream != null)
                {
                    var response = await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);
                    if (response.isSuccess == true)
                    {
                        JS.ToastrSuccess("User Reactivated Successfully !!!");
                        return true;
                    }
                    else
                    {
                        JS.ToastrError("Something Went Wrong !!!");
                    }
                }
                else
                {
                    JS.ToastrError("Something Went Wrong !!!");
                }
            }
            return false;
        }
    }
}
