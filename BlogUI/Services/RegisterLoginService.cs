using EcorpUI.Models;
using EcorpUI.Extensions;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace EcorpUI.Services
{
    public class RegisterLoginService
    {
        private readonly IJSRuntime _jsruntime;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;
        public RegisterLoginService(IConfiguration configuration, ILocalStorageService localStorageService,IJSRuntime jSRuntime) 
        {
            _localStorageService = localStorageService;
            _configuration = configuration;
            _jsruntime = jSRuntime;
        }

        public async Task<bool> UserLogin(LoginDetail cred)
        {
            HttpClient httpClient = new HttpClient();
            var requestUrl = _configuration["APIBaseUrl"] + "account/login";

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                Email = cred.Email.Trim(),
                Password = @cred.Password.Trim()
            }),
            Encoding.UTF8,
            "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, jsonContent);

            httpClient.Dispose();
            using var responseStream = await response.Content.ReadAsStreamAsync();

            UserContext context = await JsonSerializer.DeserializeAsync<UserContext>(responseStream);

            if (!string.IsNullOrEmpty(context.tokenId))
            {
                await _localStorageService.SetItemAsync("userContext", context);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordModel password)
        {
            HttpClient httpClient = new HttpClient();
            var requestUrl = _configuration["APIBaseUrl"] + "account/changepassword";

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                UserId = password.UserId,
                Password = password.Password,
                CurrentPassword = password.CurrentPassword,
                ConfirmPassword = password.ConfirmPassword,
            }),
            Encoding.UTF8,
            "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(requestUrl, jsonContent);

            httpClient.Dispose();
            using var responseStream = await response.Content.ReadAsStreamAsync();

            ResponseModel responseModel = await JsonSerializer.DeserializeAsync<ResponseModel>(responseStream);

            if (responseModel.isSuccess == true)
            {
                _jsruntime.ToastrSuccess("Password Changed Successful");
                return true;
            }
            else
            {
                _jsruntime.ToastrSuccess("Something Went Wrong");
                return false;
            }
        }
    }
}
