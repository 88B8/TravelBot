using TravelBot.Services.Contracts.Models.Auth;

namespace TravelBot.Services.Contracts.Auth;

/// <summary>
///     Сервис аутентификации
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Аутентификация
    /// </summary>
    Task<string?> Login(LoginRequestModel request, CancellationToken cancellationToken);
}