using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания и редактирования посещенного места
    /// </summary>
    public class PassportPlaceCreateModel : PassportPlaceBaseModel
    {
        /// <summary>
        /// Идентификатор места
        /// </summary>
        public Guid PlaceId { get; set; }
    }
}