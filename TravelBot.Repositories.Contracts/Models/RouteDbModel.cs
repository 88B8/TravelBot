using TravelBot.Entities.Enums;

namespace TravelBot.Repositories.Contracts.Models;

/// <summary>
///     Модель маршрута со связанными сущностями
/// </summary>
public class RouteDbModel
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

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
    public Season Season { get; set; }

    /// <summary>
    ///     Бюджет
    /// </summary>
    public string Budget { get; set; } = string.Empty;

    /// <summary>
    /// Флаг активности
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    ///     Среднее время
    /// </summary>
    public int AverageTime { get; set; }

    /// <summary>
    ///     Навигационное свойство
    /// </summary>
    public IEnumerable<PlaceDbModel> Places { get; set; } = null!;
}