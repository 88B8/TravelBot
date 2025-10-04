using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель паспорта
    /// </summary>
    public class PassportApiModel : PassportBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство посещенных мест
        /// </summary>
        public List<PlaceApiModel> Places { get; set; } = new List<PlaceApiModel>();
    }
}
