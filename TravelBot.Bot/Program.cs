using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TravelBot.Bot.Services;
using TravelBot.Common.Contracts;
using TravelBot.Common.Infrastructure;
using TravelBot.Context;
using TravelBot.Context.Contracts;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Repositories.ReadRepositories;
using TravelBot.Repositories.WriteRepositories;

public class Program
{
    public static async Task Main(string[] args)
    {
        var connectionString = "Host=localhost;Port=5432;Database=TravelBot;Username=postgres;Password=123";

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var token = "8442624105:AAHpcuNVhrp_VVXe1groquj62U_ToNIBPA0";

                services.AddDbContext<TravelBotContext>(options =>
                    options.UseNpgsql(connectionString)
                        .LogTo(Console.WriteLine));

                services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<TravelBotContext>());
                services.AddScoped<IReader>(x => x.GetRequiredService<TravelBotContext>());
                services.AddScoped<IWriter>(x => x.GetRequiredService<TravelBotContext>());
                services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
                services.AddScoped<IUserReadRepository, UserReadRepository>();
                services.AddScoped<IRouteReadRepository, RouteReadRepository>();
                services.AddScoped<IUserWriteRepository, UserWriteRepository>();
                services.AddScoped<IPlaceReadRepository, PlaceReadRepository>();
                services.AddScoped<IPassportWriteRepository, PassportWriteRepository>();
                services.AddScoped<IPassportPlaceWriteRepository, PassportPlaceWriteRepository>();

                services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(token));
                services.AddHostedService<BotService>();
            })
            .Build();

        await host.RunAsync();
    }
}