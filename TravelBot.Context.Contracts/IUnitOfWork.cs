namespace TravelBot.Context.Contracts;

/// <summary>
///     Интерфейс unit of work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Асинхронно сохранить изменения
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}