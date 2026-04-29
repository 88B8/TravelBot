using System.Net.Http.Headers;
using Blazored.LocalStorage;
using TravelBot.Api.Client.Client;
using TravelBot.Api.Client.Services;
using TravelBot.Constants;

namespace TravelBot.Web.Services;

/// <inheritdoc cref="IClientAuthService" />
public class ClientAuthService : IClientAuthService
{
    private readonly ITravelBotApiClient apiClient;
    private readonly ILocalStorageService localStorage;
    private readonly HttpClient httpClient;

    /// <summary>
    /// ctor
    /// </summary>
    public ClientAuthService(
        ITravelBotApiClient apiClient,
        ILocalStorageService localStorage,
        HttpClient httpClient)
    {
        this.apiClient = apiClient;
        this.localStorage = localStorage;
        this.httpClient = httpClient;
    }

    async Task<string?> IClientAuthService.LoginUser(
        LoginRequestApiModel request,
        CancellationToken cancellationToken)
    {
        var token = await apiClient.AuthLoginAsync(request, cancellationToken);

        if (string.IsNullOrWhiteSpace(token))
            return null;

        await localStorage.SetItemAsync(AuthConstants.TokenKey, token, cancellationToken);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return token;
    }

    async Task IClientAuthService.Logout(CancellationToken cancellationToken)
    {
        await localStorage.RemoveItemAsync(AuthConstants.TokenKey, cancellationToken);

        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}