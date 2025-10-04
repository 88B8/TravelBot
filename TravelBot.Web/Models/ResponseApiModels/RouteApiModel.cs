using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель маршрута
    /// </summary>
    public class RouteApiModel : RouteBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public IEnumerable<PlaceApiModel> Places { get; set; } = null!;
    }
}