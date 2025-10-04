using TravelBot.Services.Contracts.Models.Enums;

namespace TravelBot.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель маршрута
    /// </summary>
    public abstract class RouteBaseModel
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
        public SeasonModel Season { get; set; }

        /// <summary>
        /// Бюджет
        /// </summary>
        public string Budget { get; set; } = string.Empty;

        /// <summary>
        /// Среднее время
        /// </summary>
        public int AverageTime { get; set; }
    }
}