using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания и редактирования маршрут-место
    /// </summary>
    public class RoutePlaceCreateModel : RoutePlaceBaseModel
    {
        /// <summary>
        /// Идентификатор места
        /// </summary>
        public Guid PlaceId { get; set; }
    }
}
