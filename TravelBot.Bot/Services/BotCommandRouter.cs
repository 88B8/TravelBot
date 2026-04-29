namespace TravelBot.Bot.Services;

public sealed class BotCommandRouter
{
    public const string ShowRoutesCommand = "🗺 Показать маршруты";
    public const string PassportCommand = "📄 Мой паспорт";
    public const string HelpCommand = "❓ Помощь";
    public const string ContactsCommand = "☎️ Контакты";

    private readonly RouteMessageService routeMessageService;
    private readonly PassportService passportService;
    private readonly TelegramMessageSender sender;

    public BotCommandRouter(
        RouteMessageService routeMessageService,
        PassportService passportService,
        TelegramMessageSender sender)
    {
        this.routeMessageService = routeMessageService;
        this.passportService = passportService;
        this.sender = sender;
    }

    public async Task Handle(long telegramId, string text, CancellationToken cancellationToken)
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