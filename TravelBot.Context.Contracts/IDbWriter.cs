using System.Diagnostics.CodeAnalysis;

namespace TravelBot.Context.Contracts
{
    /// <summary>
    /// Интерфейс для записей в хранилище
    /// </summary>
    public interface IDbWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
