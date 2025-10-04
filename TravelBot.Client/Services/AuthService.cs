using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using TravelBot.Client.Contracts.Services;
using TravelBot.Shared.Models;

namespace TravelBot.Client.Services
{
    /// <inheritdoc cref="IAuthService"/>
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        /// <summary>
        /// ctor
        /// </summary>
        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<bool> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            var response = await httpClient.PostAsJsonAsync("/Login/", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var token = await response.Content.ReadAsStringAsync(cancellationToken);
            await localStorage.SetItemAsync("token", token, cancellationToken);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return true;
        }

        public async Task Logout(CancellationToken cancellationToken)
        {
            await localStorage.RemoveItemAsync("token", cancellationToken);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
