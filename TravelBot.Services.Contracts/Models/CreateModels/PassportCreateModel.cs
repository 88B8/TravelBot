using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels
{
    /// <summary>
    /// Модель создания и редактирования паспорта
    /// </summary>
    public class PassportCreateModel : PassportBaseModel
    {
        /// <summary>
        /// Посещенные места
        /// </summary>
        public List<Guid> PlaceIds { get; set; } = null!;
    }
}