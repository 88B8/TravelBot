using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель паспорта
    /// </summary>
    public class PassportModel : PassportBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство посещенных мест
        /// </summary>
        public ICollection<PlaceModel> Places { get; set; } = new List<PlaceModel>();
    }
}
