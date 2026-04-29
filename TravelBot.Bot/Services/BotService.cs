using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TravelBot.Bot.Contracts.Services;

namespace TravelBot.Bot.Services;

/// <summary>
/// Фоновый сервис бота
/// </summary>
public sealed class BotService : BackgroundService
{
    private readonly ITelegramBotClient botClient;
    private readonly ITelegramUpdateHandler updateHandler;
    private readonly ILogger<BotService> logger;

    /// <summary>
    /// ctor
    /// </summary>
    public BotService(
        ITelegramBotClient botClient,
        ITelegramUpdateHandler updateHandler,
        ILogger<BotService> logger)
    {
        this.botClient = botClient;
        this.updateHandler = updateHandler;
        this.logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        botClient.StartReceiving(
            updateHandler: updateHandler.HandleUpdateSafe,
            errorHandler: HandlePollingError,
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message]
            },
            cancellationToken: cancellationToken);

        logger.LogInformation("Telegram bot started");
        return Task.CompletedTask;
    }

    private Task HandlePollingError(
        ITelegramBotClient client,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        var message = exception switch
        {
            ApiRequestException apiException =>
                $"Telegram API error [{apiException.ErrorCode}]: {apiException.Message}",
            _ => exception.Message
        };

        logger.LogError(exception, "Polling error from {Source}: {Message}", source, message);
        return Task.CompletedTask;
    }
}