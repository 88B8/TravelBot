using TravelBot.Api.Client.Client;
using TravelBot.Bot.Helpers;

namespace TravelBot.Bot.Services;

public sealed class RouteMessageService
{
    private readonly ITravelBotApiClient apiClient;
    private readonly TelegramMessageSender sender;

    /// <summary>
    /// ctor
    /// </summary>
    public RouteMessageService(
        ITravelBotApiClient apiClient,
        TelegramMessageSender sender)
    {
        this.apiClient = apiClient;
        this.sender = sender;
    }

    public async Task ShowRoutes(long telegramId, CancellationToken cancellationToken)
    {
        var routes = await apiClient.RouteGetAllActiveAsync(cancellationToken);

        if (routes.Count == 0)
        {
            await sender.SendText(telegramId, "Маршрутов пока нет.", cancellationToken);
            return;
        }

        foreach (var route in routes)
        {
            var places = route.Places?.ToArray() ?? [];

            var placesText = places.Length == 0
                ? "Места не указаны"
                : string.Join('\n', places.Select(place =>
                    $"• {HtmlHelper.Encode(place.Name)}"));

            var message =
                "🌟 <b>Маршрут</b>\n\n" +
                $"📍 <b>Отправная точка:</b> {HtmlHelper.Encode(route.StartPoint)}\n" +
                $"🕒 <b>Среднее время:</b> {route.AverageTime} мин\n" +
                $"💰 <b>Бюджет:</b> {HtmlHelper.Encode(route.Budget)}\n" +
                $"🌤 <b>Сезон:</b> {HtmlHelper.Encode(SeasonNameProvider.GetName(route.Season))}\n" +
                $"✨ <b>Причина посетить:</b> {HtmlHelper.Encode(route.ReasonToVisit)}\n\n" +
                $"🏞 <b>Места:</b>\n{placesText}";

            await sender.SendText(telegramId, message, cancellationToken);
        }
    }
}