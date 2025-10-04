using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель маршрута
    /// </summary>
    public class RouteModel : RouteBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public IEnumerable<PlaceModel> Places { get; set; } = null!;
    }
}
