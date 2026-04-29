using TravelBot.Api.Client.Client;

namespace TravelBot.Bot.Helpers;

public static class SeasonNameProvider
{
    public static string GetName(SeasonApiModel season)
    {
        return season switch
        {
            SeasonApiModel._0 => "Круглый год",
            SeasonApiModel._1 => "Зима",
            SeasonApiModel._2 => "Лето",
            SeasonApiModel._3 => "Лето-осень",
            SeasonApiModel._4 => "Весна-осень",
            _ => "Не указан"
        };
    }
}