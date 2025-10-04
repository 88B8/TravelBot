using TravelBot.Entities.BaseModels;
using TravelBot.Entities.Enums;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель маршрута
    /// </summary>
    public class Route : BaseAuditEntity
    {
        /// <summary>
        /// Причина посетить
        /// </summary>
        public string ReasonToVisit { get; set; } = string.Empty;

        /// <summary>
        /// Отправная точка
        /// </summary>
        public string StartPoint { get; set; } = string.Empty;

        /// <summary>
        /// Сезон
        /// </summary>
        public Season Season { get; set; }

        /// <summary>
        /// Бюджет
        /// </summary>
        public string Budget { get; set; } = string.Empty;

        /// <summary>
        /// Среднее время
        /// </summary>
        public int AverageTime { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public IEnumerable<RoutePlace> RoutePlaces { get; set; } = null!;
    }
}
