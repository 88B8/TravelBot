using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель для связи many-to-many
    /// </summary>
    public class RoutePlace : BaseSoftDeletedEntity
    {
        /// <summary>
        /// Идентификатор маршрута
        /// </summary>
        public Guid RouteId { get; set; }

        /// <summary>
        /// Навигационное свойство маршрута
        /// </summary>
        public Route Route { get; set; } = null!;

        /// <summary>
        /// Идентификатор места
        /// </summary>
        public Guid PlaceId { get; set; }

        /// <summary>
        /// Навигационное свойство места
        /// </summary>
        public Place Place { get; set; } = null!;
    }
}