using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.CreateApiModels
{
    /// <summary>
    /// API модель создания и редактирования паспорта
    /// </summary>
    public class PassportCreateApiModel : PassportBaseApiModel
    {
        /// <summary>
        /// Посещенные места
        /// </summary>
        public List<Guid> PlaceIds { get; set; } = new List<Guid>();
    }
}