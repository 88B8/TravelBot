using TravelBot.Bot.Anchors;
using TravelBot.Bot.Contracts.Services;
using TravelBot.Constants;

namespace TravelBot.Bot.Services;

/// <inheritdoc cref="IBotCommandRouter"/>
public sealed class BotCommandRouter : IBotCommandRouter, IBotServiceAnchor
{
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
            case BotConstants.ShowRoutesCommand:
                await routeMessageService.ShowRoutes(telegramId, cancellationToken);
                break;

            case BotConstants.PassportCommand:
                await passportService.ShowPassport(telegramId, cancellationToken);
                break;

            case BotConstants.HelpCommand:
                await sender.SendHelp(telegramId, cancellationToken);
                break;

            case BotConstants.ContactsCommand:
                await sender.SendContacts(telegramId, cancellationToken);
                break;

            default:
                await sender.SendText(
                    telegramId,
                    BotConstants.IncorrectCommandText,
                    cancellationToken);

                await sender.SendMainKeyboard(telegramId, cancellationToken);
                break;
        }
    }
}