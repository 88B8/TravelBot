using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TravelBot.Api.Client.Client;
using TravelBot.Bot.Services;

namespace TravelBot.Bot.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTravelBot(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTelegramBot(configuration);
        services.AddTravelBotApi(configuration);
        services.AddBotServices();

        services.AddHostedService<BotService>();
    }

    private static void AddTelegramBot(this IServiceCollection services,
        IConfiguration configuration)
    {
        var telegramToken = configuration["Telegram:Token"];

        if (string.IsNullOrWhiteSpace(telegramToken))
            throw new InvalidOperationException("Telegram token не найден в конфигурации.");

        services.AddSingleton<ITelegramBotClient>(_ =>
            new TelegramBotClient(telegramToken));
    }

    private static void AddTravelBotApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        var apiBaseUrl = configuration["Api:BaseUrl"];

        if (string.IsNullOrWhiteSpace(apiBaseUrl))
            throw new InvalidOperationException("Api BaseUrl не найден в конфигурации.");

        services.AddHttpClient("TravelBotApi", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddSingleton<ITravelBotApiClient>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("TravelBotApi");

            return new TravelBotApiClient(apiBaseUrl, httpClient);
        });
    }

    private static void AddBotServices(this IServiceCollection services)
    {
        services.AddSingleton<RegistrationStateStore>();

        services.AddScoped<TelegramUpdateHandler>();
        services.AddScoped<TelegramMessageSender>();
        services.AddScoped<UserRegistrationService>();
        services.AddScoped<PassportService>();
        services.AddScoped<RouteMessageService>();
        services.AddScoped<BotCommandRouter>();
    }
}