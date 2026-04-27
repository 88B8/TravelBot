
using TravelBot.Api.Client.Client;

namespace TravelBot.Api.Client.Services;

/// <summary>
///     Сервис аутентификации
/// </summary>
public interface IClientAuthService
{
    /// <summary>
    ///     Логин
    /// </summary>
    Task<string?> LoginUser(LoginRequestApiModel request, CancellationToken cancellationToken);

    /// <summary>
    ///     Выход
    /// </summary>
    Task Logout(CancellationToken cancellationToken);
}