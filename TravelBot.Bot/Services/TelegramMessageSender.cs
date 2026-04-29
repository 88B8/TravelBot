using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TravelBot.Bot.Services;

public sealed class TelegramMessageSender
{
    private readonly ITelegramBotClient botClient;

    /// <summary>
    /// ctor
    /// </summary>
    public TelegramMessageSender(ITelegramBotClient botClient)
    {
        this.botClient = botClient;
    }

    public async Task SendText(
        long chatId,
        string text,
        CancellationToken cancellationToken,
        bool removeKeyboard = false)
    {
        ReplyMarkup? replyMarkup = removeKeyboard
            ? new ReplyKeyboardRemove()
            : null;

        await botClient.SendMessage(
            chatId: chatId,
            text: text,
            parseMode: ParseMode.Html,
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken);
    }

    public async Task SendMainKeyboard(long telegramId, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup([
            [BotCommandRouter.ShowRoutesCommand, BotCommandRouter.PassportCommand],
            [BotCommandRouter.HelpCommand, BotCommandRouter.ContactsCommand]
        ])
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false,
            IsPersistent = true
        };

        await botClient.SendMessage(
            chatId: telegramId,
            text: "Выберите действие 👇",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken);
    }

    public Task SendHelp(long telegramId, CancellationToken cancellationToken)
    {
        const string message =
            "❓ <b>Помощь</b>\n\n" +
            "🗺 <b>Показать маршруты</b> — посмотреть доступные маршруты.\n" +
            "📄 <b>Мой паспорт</b> — увидеть посещённые места.\n\n" +
            "Чтобы добавить место, отсканируйте QR-код на локации.";

        return SendText(telegramId, message, cancellationToken);
    }

    public Task SendContacts(long telegramId, CancellationToken cancellationToken)
    {
        const string message =
            "☎️ <b>Контакты</b>\n\n" +
            "По вопросам работы бота обратитесь к администратору.";

        return SendText(telegramId, message, cancellationToken);
    }
}