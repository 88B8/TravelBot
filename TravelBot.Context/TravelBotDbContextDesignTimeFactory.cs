using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelBot.Context;

/// <summary>
///     Фабрика контекста в Design Time
/// </summary>
public class TravelBotDbContextDesignTimeFactory : IDesignTimeDbContextFactory<TravelBotContext>
{
    /// <summary>
    ///     Creates a new instance of a derived context
    /// </summary>
    /// <remarks>
    ///     dotnet ef migrations add Initial --project TravelBot.Context\TravelBot.Context.csproj
    ///     dotnet ef database update --project TravelBot.Context\TravelBot.Context.csproj --connection
    ///     "Host=localhost;Port=5432;Database=TravelBot;Username=postgres;Password=123"
    /// </remarks>
    public TravelBotContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseNpgsql()
            .LogTo(Console.WriteLine)
            .Options;

        return new TravelBotContext(options);
    }
}