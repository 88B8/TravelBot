using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels;

/// <summary>
///     Модель создания и редактирования пользователя
/// </summary>
public class UserCreateModel : UserBaseModel
{
    /// <summary>
    ///     Идентификатор паспорта
    /// </summary>
    public Guid PassportId { get; set; }
}