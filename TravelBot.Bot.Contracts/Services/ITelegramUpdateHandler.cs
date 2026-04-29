using Telegram.Bot;
using Telegram.Bot.Types;

namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Обработчик обновлений сообщений
/// </summary>
public interface ITelegramUpdateHandler
{
    /// <summary>
    /// Безопасно обработать обновление
    /// </summary>
    Task HandleUpdateSafe(
        ITelegramBotClient bot,
        Update update,
        CancellationToken cancellationToken);
}