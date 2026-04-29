namespace TravelBot.Bot.Helpers;

public static class StartCommandParser
{
    public static Guid? TryGetPlaceId(string text)
    {
        var parts = text.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        return parts.Length == 2 && Guid.TryParse(parts[1], out var placeId)
            ? placeId
            : null;
    }
}