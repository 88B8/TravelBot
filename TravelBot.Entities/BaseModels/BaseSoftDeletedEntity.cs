using TravelBot.Entities.Contracts;

namespace TravelBot.Entities.BaseModels;

/// <summary>
///     Базовая модель с мягким удалением
/// </summary>
public abstract class BaseSoftDeletedEntity : IEntityWithId, IEntitySoftDeleted
{
    /// <summary>
    ///     Дата удаления
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}