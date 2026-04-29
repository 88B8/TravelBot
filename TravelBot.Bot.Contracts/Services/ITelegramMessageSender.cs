namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Сервис отправки сообщений 
/// </summary>
public interface ITelegramMessageSender
{
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    Task SendText(
        long chatId,
        string text,
        CancellationToken cancellationToken,
        bool removeKeyboard = false);

    /// <summary>
    /// Показать главную клавиатуру
    /// </summary>
    Task SendMainKeyboard(long telegramId, CancellationToken cancellationToken);

    /// <summary>
    /// Отправить справку о помощи
    /// </summary>
    Task SendHelp(long telegramId, CancellationToken cancellationToken);

    /// <summary>
    /// Отправить контакты
    /// </summary>
    Task SendContacts(long telegramId, CancellationToken cancellationToken);
}