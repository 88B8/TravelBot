using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TravelBot.Api.Client.Client;
using TravelBot.Bot.Services;

namespace TravelBot.Bot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        const string telegramToken = "8793597527:AAH4tZdCXVjAxieWOFLkoOzZ4VX8pWiUaR0";
        const string apiBaseUrl = "https://localhost:7288/";

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<ITelegramBotClient>(_ =>
                    new TelegramBotClient(telegramToken));

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

                services.AddSingleton<RegistrationStateStore>();

                services.AddScoped<TelegramUpdateHandler>();
                services.AddScoped<TelegramMessageSender>();
                services.AddScoped<UserRegistrationService>();
                services.AddScoped<PassportService>();
                services.AddScoped<RouteMessageService>();
                services.AddScoped<BotCommandRouter>();

                services.AddHostedService<BotService>();
            })
            .Build();

        await host.RunAsync();
    }
}