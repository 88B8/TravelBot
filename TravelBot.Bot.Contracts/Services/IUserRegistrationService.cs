namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Сервис регистрации пользователя
/// </summary>
public interface IUserRegistrationService
{
    /// <summary>
    /// Флаг регистрации
    /// </summary>
    Task<bool> IsRegistered(long telegramId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Онбординг
    /// </summary>
    Task Onboard(long telegramId, Guid? pendingPlaceId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Завершить регистрацию
    /// </summary>
    Task CompleteRegistration(long telegramId, string name, Guid? pendingPlaceId, CancellationToken cancellationToken);
}