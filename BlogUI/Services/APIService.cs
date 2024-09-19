using Blazored.Toast.Services;
using EcorpUI.Models;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
using System.Text.Json;
using EcorpUI.Extensions;
using Microsoft.JSInterop;

namespace EcorpUI.Services
{
    public class APIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly NavigationManager _navigationManager;
        private readonly IConfiguration _config;
        private readonly Common _common;
        private readonly IJSRuntime _jSRuntime;
        public APIService(IConfiguration config, IHttpClientFactory clientFactory, NavigationManager navigationManager,Common common,IJSRuntime jSRuntime)
        {
            _common = common;
            _clientFactory = clientFactory;
            _navigationManager = navigationManager;
            _config = config;
            _jSRuntime = jSRuntime;
        }
        public async Task<Stream> SendAsync(string url, bool isAuthenticated = true)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            UserContext context = await _common.GetUserContext();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.tokenId);

            HttpResponseMessage response = await httpClient.SendAsync(request);
            httpClient.Dispose();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else if (response.ReasonPhrase == "Forbidden")
            {
                //_navigationManager.NavigateTo("/unauthorizedpage"); // redirect to unauthorized page
            }
            else if (response.ReasonPhrase == "Unauthorized")
            {
            }
            else
            {
            }
            return null;
        }

        public async Task<Stream> PostAsync(string url, StringContent jsonContent)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            UserContext userContext = await _common.GetUserContext();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userContext.tokenId);

            HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);
            httpClient.Dispose();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else if (response.ReasonPhrase == "Forbidden")
            {
                _jSRuntime.ToastsWarning("Unauthorized");
            }
            else if (response.ReasonPhrase == "Unauthorized")
            {
                _jSRuntime.ToastsWarning("Login to continue");
                _navigationManager.NavigateTo("/Login"); // redirect to login page
            }
            else
            {
                _jSRuntime.ToastsWarning(response.ReasonPhrase);
            }
            return null;

        }

        public async Task<Boolean> DeleteAsync(string url)
        {
            HttpClient httpClient = _clientFactory.CreateClient();
            UserContext userContext = await _common.GetUserContext();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userContext.tokenId);

            HttpResponseMessage response = await httpClient.DeleteAsync(url);
            httpClient.Dispose();
            try
            {
                ResponseModel responseModel = await JsonSerializer.DeserializeAsync<ResponseModel>(await response.Content.ReadAsStreamAsync());

                if (response.IsSuccessStatusCode)
                {
                    return (bool)responseModel.isSuccess;
                }
                else if (response.ReasonPhrase == "Forbidden")
                {
                    _jSRuntime.ToastsWarning("Unauthorized");
                }
                else if (response.ReasonPhrase == "Unauthorized")
                {
                    _jSRuntime.ToastsWarning("Login to continue");
                    _navigationManager.NavigateTo("/Account/Login"); // redirect to login page
                }
                else
                {
                    _jSRuntime.ToastrError(responseModel.errorMessage);
                }
            }
            catch (Exception ex)
            {
                _jSRuntime.ToastrError("Something Went Wrong !!!");
            }
            return false;
        }
    }
}
