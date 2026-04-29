using TravelBot.Bot.Anchors;
using TravelBot.Bot.Contracts.Services;

namespace TravelBot.Bot.Services;

/// <inheritdoc cref="IBotCommandRouter"/>
public sealed class BotCommandRouter : IBotCommandRouter, IBotServiceAnchor
{
    public const string ShowRoutesCommand = "🗺 Показать маршруты";
    public const string PassportCommand = "📄 Мой паспорт";
    public const string HelpCommand = "❓ Помощь";
    public const string ContactsCommand = "☎️ Контакты";

    private readonly IRouteMessageService routeMessageService;
    private readonly IPassportService passportService;
    private readonly ITelegramMessageSender sender;

    /// <summary>
    /// ctor
    /// </summary>
    public BotCommandRouter(
        IRouteMessageService routeMessageService,
        IPassportService passportService,
        ITelegramMessageSender sender)
    {
        this.routeMessageService = routeMessageService;
        this.passportService = passportService;
        this.sender = sender;
    }

    async Task IBotCommandRouter.Handle(long telegramId, string text, CancellationToken cancellationToken)
    {
        switch (text)
        {
            case ShowRoutesCommand:
            case "Показать маршруты":
                await routeMessageService.ShowRoutes(telegramId, cancellationToken);
                break;

            case PassportCommand:
            case "Мой паспорт":
                await passportService.ShowPassport(telegramId, cancellationToken);
                break;

            case HelpCommand:
            case "Помощь":
                await sender.SendHelp(telegramId, cancellationToken);
                break;

            case ContactsCommand:
            case "Контакты":
                await sender.SendContacts(telegramId, cancellationToken);
                break;

            default:
                await sender.SendText(
                    telegramId,
                    "Неизвестная команда. Выберите действие на клавиатуре 👇",
                    cancellationToken);

                await sender.SendMainKeyboard(telegramId, cancellationToken);
                break;
        }
    }
}