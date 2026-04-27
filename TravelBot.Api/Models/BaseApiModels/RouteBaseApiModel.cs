using TravelBot.Api.Models.Enums;

namespace TravelBot.Api.Models.BaseApiModels;

/// <summary>
///     Базовая API модель маршрута
/// </summary>
public abstract class RouteBaseApiModel
{
    /// <summary>
    ///     Причина посетить
    /// </summary>
    public string ReasonToVisit { get; set; } = string.Empty;

    /// <summary>
    ///     Отправная точка
    /// </summary>
    public string StartPoint { get; set; } = string.Empty;

    /// <summary>
    ///     Сезон
    /// </summary>
    public SeasonApiModel Season { get; set; }

    /// <summary>
    ///     Бюджет
    /// </summary>
    public string Budget { get; set; } = string.Empty;

    /// <summary>
    ///     Среднее время
    /// </summary>
    public int AverageTime { get; set; }
}