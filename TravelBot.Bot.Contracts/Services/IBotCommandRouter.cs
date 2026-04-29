namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Маршрутизатор команд бота
/// </summary>
public interface IBotCommandRouter
{
    /// <summary>
    /// Обработать команду
    /// </summary>
    Task Handle(long telegramId, string text, CancellationToken cancellationToken);
}