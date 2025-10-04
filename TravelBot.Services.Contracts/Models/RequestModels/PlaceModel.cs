using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель места
    /// </summary>
    public class PlaceModel : PlaceBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public CategoryModel Category { get; set; } = null!;
    }
}
