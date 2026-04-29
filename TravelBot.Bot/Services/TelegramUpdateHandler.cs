using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TravelBot.Bot.Helpers;

namespace TravelBot.Bot.Services;

public sealed class TelegramUpdateHandler
{
    private readonly UserRegistrationService registrationService;
    private readonly RegistrationStateStore registrationStateStore;
    private readonly PassportService passportService;
    private readonly BotCommandRouter commandRouter;
    private readonly TelegramMessageSender sender;
    private readonly ILogger<TelegramUpdateHandler> logger;

    /// <summary>
    /// ctor
    /// </summary>
    public TelegramUpdateHandler(
        UserRegistrationService registrationService,
        RegistrationStateStore registrationStateStore,
        PassportService passportService,
        BotCommandRouter commandRouter,
        TelegramMessageSender sender,
        ILogger<TelegramUpdateHandler> logger)
    {
        this.registrationService = registrationService;
        this.registrationStateStore = registrationStateStore;
        this.passportService = passportService;
        this.commandRouter = commandRouter;
        this.sender = sender;
        this.logger = logger;
    }

    public async Task HandleUpdateSafe(
        ITelegramBotClient bot,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            await HandleUpdate(update, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to process update {UpdateId}", update.Id);

            if (update.Message?.Chat.Id is { } chatId)
            {
                await sender.SendText(
                    chatId,
                    "⚠️ Произошла ошибка. Попробуйте ещё раз чуть позже.",
                    cancellationToken);
            }
        }
    }

    private async Task HandleUpdate(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        if (message?.Text is null || message.From is null)
            return;

        var telegramId = message.From.Id;
        var text = message.Text.Trim();

        if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
        {
            await HandleStart(telegramId, text, cancellationToken);
            return;
        }

        if (registrationStateStore.TryGet(telegramId, out var state))
        {
            await registrationService.CompleteRegistration(
                telegramId,
                text,
                state.PendingPlaceId,
                cancellationToken);

            return;
        }

        if (!await registrationService.IsRegistered(telegramId, cancellationToken))
        {
            await registrationService.AskName(telegramId, null, cancellationToken);
            return;
        }

        await commandRouter.Handle(telegramId, text, cancellationToken);
    }

    private async Task HandleStart(
        long telegramId,
        string text,
        CancellationToken cancellationToken)
    {
        var placeId = StartCommandParser.TryGetPlaceId(text);

        if (!await registrationService.IsRegistered(telegramId, cancellationToken))
        {
            await registrationService.AskName(telegramId, placeId, cancellationToken);
            return;
        }

        if (placeId is not null)
            await passportService.AddPlaceToPassport(telegramId, placeId.Value, cancellationToken);

        await sender.SendMainKeyboard(telegramId, cancellationToken);
    }
}