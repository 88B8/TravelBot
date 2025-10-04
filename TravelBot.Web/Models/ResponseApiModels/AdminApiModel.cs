using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель администратора
    /// </summary>
    public class AdminApiModel : AdminBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}