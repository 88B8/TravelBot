using TravelBot.Shared.Models;

namespace TravelBot.Services.Contracts.Auth
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Аутентификация
        /// </summary>
        Task<string?> Login(LoginRequestModel request, CancellationToken cancellationToken);
    }
}
