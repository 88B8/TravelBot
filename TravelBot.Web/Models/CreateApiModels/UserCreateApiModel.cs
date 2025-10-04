using TravelBot.Web.Models.BaseApiModels;

namespace TravelBot.Web.Models.CreateApiModels
{
    /// <summary>
    /// API модель создания и редактирования пользователя
    /// </summary>
    public class UserCreateApiModel : UserBaseApiModel
    {
        /// <summary>
        /// Идентификатор паспорта
        /// </summary>
        public Guid PassportId { get; set; }
    }
}
