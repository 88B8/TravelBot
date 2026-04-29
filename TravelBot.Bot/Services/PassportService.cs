using TravelBot.Api.Client.Client;
using TravelBot.Bot.Helpers;

namespace TravelBot.Bot.Services;

/// <summary>
/// Сервис работы с паспортом пользователя
/// </summary>
public sealed class PassportService
{
    private readonly ITravelBotApiClient apiClient;
    private readonly TelegramMessageSender sender;

    /// <summary>
    /// ctor
    /// </summary>
    public PassportService(
        ITravelBotApiClient apiClient,
        TelegramMessageSender sender)
    {
        this.apiClient = apiClient;
        this.sender = sender;
    }

    public async Task ShowPassport(long telegramId, CancellationToken cancellationToken)
    {
        var user = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (user is null)
        {
            await sender.SendText(telegramId, "У вас нет доступа. Используйте /start", cancellationToken);
            return;
        }

        var passport = user.Passport;

        if (passport is null)
        {
            await sender.SendText(
                telegramId,
                "Не удалось найти ваш туристический паспорт.",
                cancellationToken);

            return;
        }

        var places = passport.Places?.ToArray() ?? [];

        var placesText = places.Length == 0
            ? "Нет посещённых мест."
            : string.Join('\n', places.Select(place =>
                $"• {HtmlHelper.Encode(place.Name)}"));

        var message =
            "📄 <b>Ваш туристический паспорт</b>\n\n" +
            $"🏷️ <b>Имя:</b> {HtmlHelper.Encode(user.Name)}\n\n" +
            $"🏞 <b>Посещённые места:</b>\n{placesText}";

        await sender.SendText(telegramId, message, cancellationToken);
    }

    public async Task AddPlaceToPassport(
        long telegramId,
        Guid placeId,
        CancellationToken cancellationToken)
    {
        var user = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (user is null)
        {
            await sender.SendText(telegramId, "У вас нет доступа. Используйте /start", cancellationToken);
            return;
        }

        var passport = user.Passport;

        if (passport is null)
        {
            await sender.SendText(
                telegramId,
                "Не удалось найти ваш туристический паспорт.",
                cancellationToken);

            return;
        }

        var place = await TryGetPlace(placeId, cancellationToken);

        if (place is null)
        {
            await sender.SendText(telegramId, "❌ QR-код недействителен.", cancellationToken);
            return;
        }

        var currentPlaces = passport.Places?.ToArray() ?? [];

        if (currentPlaces.Any(p => p.Id == placeId))
        {
            await sender.SendText(
                telegramId,
                $"✅ Место <b>{HtmlHelper.Encode(place.Name)}</b> уже есть в вашем туристическом паспорте!",
                cancellationToken);

            return;
        }

        var placeIds = currentPlaces
            .Select(p => p.Id)
            .Append(placeId)
            .Distinct()
            .ToArray();

        var editModel = new PassportCreateApiModel
        {
            PlaceIds = placeIds
        };

        await apiClient.PassportEditAsync(
            passport.Id,
            editModel,
            cancellationToken);

        await sender.SendText(
            telegramId,
            $"🎉 Место <b>{HtmlHelper.Encode(place.Name)}</b> добавлено в ваш туристический паспорт!",
            cancellationToken);
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

    private async Task<PlaceApiModel?> TryGetPlace(
        Guid placeId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.PlaceGetByIdAsync(placeId, cancellationToken);
        }
        catch (ApiException exception) when (exception.StatusCode == 404)
        {
            return null;
        }
    }
}