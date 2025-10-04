using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.CreateApiModels
{
    /// <summary>
    /// API модель создания и редактирования места
    /// </summary>
    public class PlaceCreateApiModel : PlaceBaseApiModel
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
