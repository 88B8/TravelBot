using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace TravelBot.Client.Services
{
    /// <summary>
    /// Кастомный поставщик состояния аутентификации
    /// </summary>
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        /// <summary>
        /// ctor
        /// </summary>
        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>("token");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                identity = new ClaimsIdentity(jwt.Claims, "jwt");
            }

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}
