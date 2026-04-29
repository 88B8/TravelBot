using System.Net;

namespace TravelBot.Bot.Helpers;

public static class HtmlHelper
{
    public static string Encode(string? value)
    {
        return WebUtility.HtmlEncode(value ?? string.Empty);
    }
}