using Microsoft.Extensions.Hosting;
using TravelBot.Bot.Extensions;

namespace TravelBot.Bot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddTravelBot(context.Configuration);
            })
            .Build();

        await host.RunAsync();
    }
}