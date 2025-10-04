using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель категории
    /// </summary>
    public class CategoryModel : CategoryBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
