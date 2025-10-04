using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания и редактирования места
    /// </summary>
    public class PlaceCreateModel : PlaceBaseModel
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
