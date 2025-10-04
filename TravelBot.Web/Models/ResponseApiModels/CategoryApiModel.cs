using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель категории
    /// </summary>
    public class CategoryApiModel : CategoryBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
