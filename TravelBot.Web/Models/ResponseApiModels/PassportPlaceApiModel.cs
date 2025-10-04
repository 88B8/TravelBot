using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель посещенного места
    /// </summary>
    public class PassportPlaceApiModel : PassportPlaceBaseApiModel
    {
        /// <summary>
        /// Навигационное свойство места
        /// </summary>
        public PlaceApiModel Place { get; set; } = null!;
    }
}
