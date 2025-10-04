using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель маршрута-места
    /// </summary>
    public class RoutePlaceApiModel : RoutePlaceBaseApiModel
    {
        /// <summary>
        /// Место
        /// </summary>
        public PlaceApiModel Place { get; set; } = null!;
    }
}
