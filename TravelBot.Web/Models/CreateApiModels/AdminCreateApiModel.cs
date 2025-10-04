using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.CreateApiModels
{
    /// <summary>
    /// API модель создания администратора
    /// </summary>
    public class AdminCreateApiModel : AdminBaseApiModel
    {
        /// <summary>
        /// Пароль администратора
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
