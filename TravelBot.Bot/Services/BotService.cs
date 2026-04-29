using System.Collections.Concurrent;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TravelBot.Api.Client.Client;

namespace TravelBot.Bot.Services;

public sealed class BotService : BackgroundService
{
    private const string ShowRoutesCommand = "🗺 Показать маршруты";
    private const string PassportCommand = "📄 Мой паспорт";
    private const string HelpCommand = "❓ Помощь";
    private const string ContactsCommand = "☎️ Контакты";

    private readonly ITelegramBotClient botClient;
    private readonly ITravelBotApiClient apiClient;
    private readonly ILogger<BotService> logger;

    private readonly ConcurrentDictionary<long, RegistrationState> registrationStates = new();

    /// <summary>
    /// ctor
    /// </summary>
    public BotService(
        ITelegramBotClient botClient,
        ITravelBotApiClient apiClient,
        ILogger<BotService> logger)
    {
        this.botClient = botClient;
        this.apiClient = apiClient;
        this.logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        botClient.StartReceiving(
            updateHandler: HandleUpdateSafe,
            errorHandler: HandlePollingError,
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message]
            },
            cancellationToken: cancellationToken);

        logger.LogInformation("Telegram bot started");
        return Task.CompletedTask;
    }

    private async Task HandleUpdateSafe(
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
                await SendText(
                    chatId,
                    "⚠️ Произошла ошибка. Попробуйте ещё раз чуть позже.",
                    cancellationToken);
            }
        }
    }

    private async Task HandleUpdate(Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Text is null)
            return;

        var message = update.Message;

        if (message.From is null)
            return;

        var userId = message.From.Id;
        var text = message.Text.Trim();

        if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
        {
            await HandleStart(userId, text, cancellationToken);
            return;
        }

        if (registrationStates.TryGetValue(userId, out var state))
        {
            await CompleteRegistration(userId, text, state.PendingPlaceId, cancellationToken);
            return;
        }

        if (!await IsUserRegistered(userId, cancellationToken))
        {
            await AskName(userId, null, cancellationToken);
            return;
        }

        await HandleCommand(userId, text, cancellationToken);
    }

    private async Task HandleStart(long telegramId, string text, CancellationToken cancellationToken)
    {
        var placeId = TryGetPlaceIdFromStartCommand(text);

        if (!await IsUserRegistered(telegramId, cancellationToken))
        {
            await AskName(telegramId, placeId, cancellationToken);
            return;
        }

        if (placeId is not null)
            await AddPlaceToPassport(telegramId, placeId.Value, cancellationToken);

        await SendMainKeyboard(telegramId, cancellationToken);
    }

    private async Task AskName(long telegramId, Guid? pendingPlaceId, CancellationToken cancellationToken)
    {
        registrationStates[telegramId] = new RegistrationState(pendingPlaceId);

        var message = pendingPlaceId is null
            ? "Привет! Как тебя зовут?"
            : "Привет! Чтобы добавить место в паспорт, сначала зарегистрируйся. Как тебя зовут?";

        await SendText(telegramId, message, cancellationToken, removeKeyboard: true);
    }

    private async Task CompleteRegistration(
        long telegramId,
        string name,
        Guid? pendingPlaceId,
        CancellationToken cancellationToken)
    {
        name = name.Trim();

        if (name.Length is < 2 or > 80)
        {
            await SendText(telegramId, "Имя должно быть от 2 до 80 символов. Попробуй ещё раз.", cancellationToken);
            return;
        }

        var existingUser = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (existingUser is null)
        {
            var passportCreateModel = new PassportCreateApiModel();
            var passport = await apiClient.PassportCreateAsync(passportCreateModel, cancellationToken);

            var userCreateModel = new UserCreateApiModel();

            SetIfExists(userCreateModel, "Name", name);
            SetIfExists(userCreateModel, "TelegramId", telegramId);
            SetIfExists(userCreateModel, "PassportId", GetGuid(passport, "Id"));
            SetIfExists(userCreateModel, "Passport", passport);

            await apiClient.UserCreateAsync(userCreateModel, cancellationToken);
        }

        registrationStates.TryRemove(telegramId, out _);

        await SendText(
            telegramId,
            $"Рад познакомиться, <b>{Html(name)}</b>! 👋 Теперь ты зарегистрирован.",
            cancellationToken);

        if (pendingPlaceId is not null)
            await AddPlaceToPassport(telegramId, pendingPlaceId.Value, cancellationToken);

        await SendMainKeyboard(telegramId, cancellationToken);
    }

    private async Task HandleCommand(long telegramId, string text, CancellationToken cancellationToken)
    {
        switch (text)
        {
            case ShowRoutesCommand:
            case "Показать маршруты":
                await ShowRoutes(telegramId, cancellationToken);
                break;

            case PassportCommand:
            case "Мой паспорт":
                await ShowPassport(telegramId, cancellationToken);
                break;

            case HelpCommand:
            case "Помощь":
                await SendHelp(telegramId, cancellationToken);
                break;

            case ContactsCommand:
            case "Контакты":
                await SendContacts(telegramId, cancellationToken);
                break;

            default:
                await SendText(
                    telegramId,
                    "Неизвестная команда. Выберите действие на клавиатуре 👇",
                    cancellationToken);
                await SendMainKeyboard(telegramId, cancellationToken);
                break;
        }
    }

    private async Task ShowRoutes(long telegramId, CancellationToken cancellationToken)
    {
        var routes = await apiClient.RouteGetAllActiveAsync(cancellationToken);

        if (routes.Count == 0)
        {
            await SendText(telegramId, "Маршрутов пока нет.", cancellationToken);
            return;
        }

        foreach (var route in routes)
        {
            var places = GetCollection<object>(route, "Places").ToArray();

            var placesText = places.Length == 0
                ? "Места не указаны"
                : string.Join('\n', places.Select(place => $"• {Html(GetString(place, "Name"))}"));

            var message =
                "🌟 <b>Маршрут</b>\n\n" +
                $"📍 <b>Отправная точка:</b> {Html(GetString(route, "StartPoint"))}\n" +
                $"🕒 <b>Среднее время:</b> {Html(GetString(route, "AverageTime"))} мин\n" +
                $"💰 <b>Бюджет:</b> {Html(GetString(route, "Budget"))}\n" +
                $"🌤 <b>Сезон:</b> {Html(GetSeasonName(route.Season))}\n" +
                $"✨ <b>Причина посетить:</b> {Html(GetString(route, "ReasonToVisit"))}\n\n" +
                $"🏞 <b>Места:</b>\n{placesText}";

            await SendText(telegramId, message, cancellationToken);
        }
    }

    private async Task ShowPassport(long telegramId, CancellationToken cancellationToken)
    {
        var user = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (user is null)
        {
            await SendText(telegramId, "У вас нет доступа. Используйте /start", cancellationToken);
            return;
        }

        var passport = GetValue(user, "Passport");

        if (passport is null)
        {
            var passportId = GetGuid(user, "PassportId");

            if (passportId is not null)
                passport = await apiClient.PassportGetByIdAsync(passportId.Value, cancellationToken);
        }

        var places = passport is null
            ? []
            : GetCollection<object>(passport, "Places").ToArray();

        var placesText = places.Length == 0
            ? "Нет посещённых мест."
            : string.Join('\n', places.Select(place => $"• {Html(GetString(place, "Name"))}"));

        var message =
            "📄 <b>Ваш туристический паспорт</b>\n\n" +
            $"🏷️ <b>Имя:</b> {Html(GetString(user, "Name"))}\n\n" +
            $"🏞 <b>Посещённые места:</b>\n{placesText}";

        await SendText(telegramId, message, cancellationToken);
    }

    private async Task AddPlaceToPassport(long telegramId, Guid placeId, CancellationToken cancellationToken)
    {
        var user = await TryGetUserByTelegramId(telegramId, cancellationToken);

        if (user is null)
        {
            await SendText(telegramId, "У вас нет доступа. Используйте /start", cancellationToken);
            return;
        }

        var place = await TryGetPlace(placeId, cancellationToken);

        if (place is null)
        {
            await SendText(telegramId, "❌ QR-код недействителен.", cancellationToken);
            return;
        }

        var passport = GetValue(user, "Passport");
        var passportId = GetGuid(user, "PassportId") ?? GetGuid(passport, "Id");

        if (passport is null && passportId is not null)
            passport = await apiClient.PassportGetByIdAsync(passportId.Value, cancellationToken);

        if (passport is null || passportId is null)
        {
            await SendText(telegramId, "Не удалось найти ваш туристический паспорт.", cancellationToken);
            return;
        }

        var currentPlaces = GetCollection<object>(passport, "Places").ToArray();

        if (currentPlaces.Any(p => GetGuid(p, "Id") == placeId))
        {
            await SendText(
                telegramId,
                $"✅ Место <b>{Html(GetString(place, "Name"))}</b> уже есть в вашем туристическом паспорте!",
                cancellationToken);
            return;
        }

        var placeIds = currentPlaces
            .Select(p => GetGuid(p, "Id"))
            .Where(id => id is not null)
            .Select(id => id!.Value)
            .Append(placeId)
            .Distinct()
            .ToArray();

        var passportEditModel = new PassportCreateApiModel();

        SetIfExists(passportEditModel, "PlaceIds", placeIds);
        SetIfExists(passportEditModel, "PlacesIds", placeIds);
        SetIfExists(passportEditModel, "Places", currentPlaces.Append(place).ToArray());

        await apiClient.PassportEditAsync(passportId.Value, passportEditModel, cancellationToken);

        await SendText(
            telegramId,
            $"🎉 Место <b>{Html(GetString(place, "Name"))}</b> добавлено в ваш туристический паспорт!",
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

    private async Task<PlaceApiModel?> TryGetPlace(Guid placeId, CancellationToken cancellationToken)
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

    private async Task<bool> IsUserRegistered(long telegramId, CancellationToken cancellationToken)
    {
        return await TryGetUserByTelegramId(telegramId, cancellationToken) is not null;
    }

    private async Task SendHelp(long telegramId, CancellationToken cancellationToken)
    {
        const string message =
            "❓ <b>Помощь</b>\n\n" +
            "🗺 <b>Показать маршруты</b> — посмотреть доступные маршруты.\n" +
            "📄 <b>Мой паспорт</b> — увидеть посещённые места.\n\n" +
            "Чтобы добавить место, отсканируйте QR-код на локации.";

        await SendText(telegramId, message, cancellationToken);
    }

    private async Task SendContacts(long telegramId, CancellationToken cancellationToken)
    {
        const string message =
            "☎️ <b>Контакты</b>\n\n" +
            "По вопросам работы бота обратитесь к администратору.";

        await SendText(telegramId, message, cancellationToken);
    }

    private async Task SendMainKeyboard(long telegramId, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup([
            [ShowRoutesCommand, PassportCommand],
            [HelpCommand, ContactsCommand]
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

    private async Task SendText(
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

    private static Guid? TryGetPlaceIdFromStartCommand(string text)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        return parts.Length == 2 && Guid.TryParse(parts[1], out var placeId)
            ? placeId
            : null;
    }

    private static object? GetValue(object? source, string propertyName)
    {
        return source?.GetType()
            .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?.GetValue(source);
    }

    private static string GetString(object? source, string propertyName)
    {
        return GetValue(source, propertyName)?.ToString() ?? string.Empty;
    }

    private static Guid? GetGuid(object? source, string propertyName)
    {
        var value = GetValue(source, propertyName);

        return value switch
        {
            Guid g => g,
            string s when Guid.TryParse(s, out var parsed) => parsed,
            _ => null
        };
    }

    private static IEnumerable<T> GetCollection<T>(object? source, string propertyName)
    {
        var value = GetValue(source, propertyName);

        if (value is null)
            return [];

        if (value is IEnumerable<T> typedCollection)
            return typedCollection;

        if (value is System.Collections.IEnumerable collection)
            return collection.Cast<object>().OfType<T>();

        return [];
    }

    private static void SetIfExists(object target, string propertyName, object? value)
    {
        var property = target.GetType()
            .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (property is null || !property.CanWrite)
            return;

        if (value is null)
        {
            property.SetValue(target, null);
            return;
        }

        var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        if (targetType.IsInstanceOfType(value))
        {
            property.SetValue(target, value);
            return;
        }

        if (targetType == typeof(string))
        {
            property.SetValue(target, value.ToString());
            return;
        }

        if (targetType == typeof(long) && long.TryParse(value.ToString(), out var longValue))
        {
            property.SetValue(target, longValue);
            return;
        }

        if (targetType == typeof(int) && int.TryParse(value.ToString(), out var intValue))
        {
            property.SetValue(target, intValue);
            return;
        }

        if (targetType == typeof(Guid) && Guid.TryParse(value.ToString(), out var guidValue))
        {
            property.SetValue(target, guidValue);
            return;
        }

        property.SetValue(target, value);
    }

    private static string? GetSeasonName(SeasonApiModel season)
    {
        return season switch
        {
            SeasonApiModel._0 => "Круглый год",
            SeasonApiModel._1 => "Зима",
            SeasonApiModel._2 => "Лето",
            SeasonApiModel._3 => "Лето-осень",
            SeasonApiModel._4 => "Весна-осень",
            _ => null
        };
    }

    private static string Html(string? value)
    {
        return WebUtility.HtmlEncode(value ?? string.Empty);
    }

    private sealed record RegistrationState(Guid? PendingPlaceId);
}