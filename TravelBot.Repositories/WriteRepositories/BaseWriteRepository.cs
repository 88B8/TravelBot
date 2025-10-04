using System.Diagnostics.CodeAnalysis;
using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities.Contracts;

namespace TravelBot.Repositories.WriteRepositories
{
    public abstract class BaseWriteRepository<T> : IDbWriter<T> where T : class
    {
        private readonly IWriter writer;
        private readonly IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// ctor
        /// </summary>
        protected BaseWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        {
            this.writer = writer;
            this.dateTimeProvider = dateTimeProvider;
        }

        void IDbWriter<T>.Add([NotNull] T entity)
        {
            CreateAudit(entity);
            UpdateAudit(entity);
            writer.Add(entity);
        }

        void IDbWriter<T>.Update([NotNull] T entity)
        {
            UpdateAudit(entity);
            writer.Update(entity);
        }

        void IDbWriter<T>.Delete([NotNull] T entity)
        {
            UpdateAudit(entity);

            if (entity is IEntitySoftDeleted softEntity)
            {
                softEntity.DeletedAt = dateTimeProvider.UtcNow();
                writer.Update(softEntity);
            }
            else
            {
                writer.Delete(entity);
            }
        }

        private void CreateAudit([NotNull] T entity)
        {
            if (entity is IEntityWithAudit auditCreated)
            {
                auditCreated.CreatedAt = dateTimeProvider.UtcNow();
            }
        }

        private void UpdateAudit([NotNull] T entity)
        {
            if (entity is IEntityWithAudit auditCreated)
            {
                auditCreated.UpdatedAt = dateTimeProvider.UtcNow();
            }
        }
    }
}
