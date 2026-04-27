namespace TravelBot.Api.Models.Enums;

/// <summary>
///     API модель сезона
/// </summary>
public enum SeasonApiModel
{
    /// <summary>
    ///     Круглый год
    /// </summary>
    AllYear = 0,

    /// <summary>
    ///     Зима
    /// </summary>
    Winter = 1,

    /// <summary>
    ///     Лето
    /// </summary>
    Summer = 2,

    /// <summary>
    ///     Лето-осень
    /// </summary>
    SummerAutumn = 3,

    /// <summary>
    ///     Весна-лето
    /// </summary>
    SpringSummer = 4
}