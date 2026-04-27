namespace TravelBot.Context.Contracts;

/// <summary>
///     Предоставляет функциональную возможность чтения из контекста
/// </summary>
public interface IReader
{
    /// <summary>
    ///     Читает записи из контекста
    /// </summary>
    IQueryable<TEntity> Read<TEntity>() where TEntity : class;
}