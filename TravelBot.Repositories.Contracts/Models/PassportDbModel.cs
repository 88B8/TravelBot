namespace TravelBot.Repositories.Contracts.Models;

/// <summary>
///     Модель паспорта с заполненными связанными сущностями
/// </summary>
public class PassportDbModel
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Места
    /// </summary>
    public IEnumerable<PlaceDbModel> Places { get; set; } = null!;
}