using TravelBot.Api.Client.Client;
using TravelBot.Bot.Helpers;

namespace TravelBot.Bot.Services;

public sealed class UserRegistrationService
{
    private readonly ITravelBotApiClient apiClient;
    private readonly RegistrationStateStore stateStore;
    private readonly PassportService passportService;
    private readonly TelegramMessageSender sender;

    public UserRegistrationService(
        ITravelBotApiClient apiClient,
        RegistrationStateStore stateStore,
        PassportService passportService,
        TelegramMessageSender sender)
    {
        this.apiClient = apiClient;
        this.stateStore = stateStore;
        this.passportService = passportService;
        this.sender = sender;
    }

    public async Task<bool> IsRegistered(long telegramId, CancellationToken cancellationToken)
    {
        return await TryGetUserByTelegramId(telegramId, cancellationToken) is not null;
    }

    private async Task<UserApiModel?> TryGetUserByTelegramId(
        long telegramId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.UserGetByTelegramIdAsync(telegramId, cancellationToken);
        }
        catch (ApiException exception) when (exception.StatusCode == 404)
        {
            return null;
        }
    }

    public async Task AskName(
        long telegramId,
        Guid? pendingPlaceId,
        CancellationToken cancellationToken)
    {
        stateStore.Set(telegramId, pendingPlaceId);

        var message = pendingPlaceId is null
            ? "Привет! Как тебя зовут?"
            : "Привет! Чтобы добавить место в паспорт, сначала зарегистрируйся. Как тебя зовут?";

        await sender.SendText(telegramId, message, cancellationToken, removeKeyboard: true);
    }

    public async Task CompleteRegistration(
        long telegramId,
        string name,
        Guid? pendingPlaceId,
        CancellationToken cancellationToken)
    {
        name = name.Trim();

        if (name.Length is < 2 or > 80)
        {
            await sender.SendText(
                telegramId,
                "Имя должно быть от 2 до 80 символов. Попробуй ещё раз.",
                cancellationToken);

            return;
        }

        var existingUser = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (existingUser is null)
        {
            var passport = await apiClient.PassportCreateAsync(
                new PassportCreateApiModel
                {
                    PlaceIds = []
                },
                cancellationToken);

            await apiClient.UserCreateAsync(
                new UserCreateApiModel
                {
                    Name = name,
                    TelegramId = telegramId,
                    PassportId = passport.Id
                },
                cancellationToken);
        }

        stateStore.Remove(telegramId);

        await sender.SendText(
            telegramId,
            $"Рад познакомиться, <b>{HtmlHelper.Encode(name)}</b>! 👋 Теперь ты зарегистрирован.",
            cancellationToken);

        if (pendingPlaceId is not null)
            await passportService.AddPlaceToPassport(telegramId, pendingPlaceId.Value, cancellationToken);

        await sender.SendMainKeyboard(telegramId, cancellationToken);
    }
}