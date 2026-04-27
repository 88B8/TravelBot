using System.Diagnostics.CodeAnalysis;

namespace TravelBot.Context.Contracts;

/// <summary>
///     Предоставляет возможность записи в контекст
/// </summary>
public interface IWriter
{
    /// <summary>
    ///     Добавляет запись в контекст
    /// </summary>
    void Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    ///     Модифицирует запись в контексте
    /// </summary>
    void Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    ///     Удаляет запись из контекста
    /// </summary>
    void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class;
}