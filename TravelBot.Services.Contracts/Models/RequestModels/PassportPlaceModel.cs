using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель посещенного места
    /// </summary>
    public class PassportPlaceModel : PassportPlaceBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство места
        /// </summary>
        public PlaceModel Place { get; set; } = null!;
    }
}