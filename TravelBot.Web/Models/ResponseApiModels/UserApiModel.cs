using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.ResponseApiModels
{
    /// <summary>
    /// API модель пользователя
    /// </summary>
    public class UserApiModel : UserBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public PassportApiModel Passport { get; set; } = null!;
    }
}
