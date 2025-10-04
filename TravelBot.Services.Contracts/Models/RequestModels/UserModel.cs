using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserModel : UserBaseModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство паспорта
        /// </summary>
        public PassportModel Passport { get; set; } = null!;
    }
}
