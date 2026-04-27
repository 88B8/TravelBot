using TravelBot.Entities.Contracts;

namespace TravelBot.Entities.BaseModels;

/// <summary>
///     Базовая модель сущности с аудитом
/// </summary>
public abstract class BaseAuditEntity : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
{
    /// <summary>
    ///     Дата удаления
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    ///     Дата создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Дата изменения
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}