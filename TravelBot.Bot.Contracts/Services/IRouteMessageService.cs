namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Сервис маршрутизации сообщений
/// </summary>
public interface IRouteMessageService
{
    /// <summary>
    /// Отобразить актуальные маршруты
    /// </summary>
    Task ShowRoutes(long telegramId, CancellationToken cancellationToken);
}