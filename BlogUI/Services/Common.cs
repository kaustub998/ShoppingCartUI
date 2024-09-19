using EcorpUI.Models;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

namespace EcorpUI.Services
{
    public class Common
    {
        private readonly ILocalStorageService _localStorageService;
        public Common(ILocalStorageService localStorageService) 
        {
            _localStorageService = localStorageService;
        }

        public async Task<UserContext> GetUserContext()
        {
            return await _localStorageService.GetItemAsync<UserContext>("userContext") ?? new UserContext();
        }

        public static bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jsonToken == null)
                {
                    return true;
                }
                var expirationTime = jsonToken.ValidTo;
                return DateTime.UtcNow >= expirationTime;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
