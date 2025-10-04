using TravelBot.Shared.Models;

namespace TravelBot.Client.Contracts.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Логин
        /// </summary>
        Task<bool> Login(LoginRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Выход
        /// </summary>
        Task Logout(CancellationToken cancellationToken);
    }
}
