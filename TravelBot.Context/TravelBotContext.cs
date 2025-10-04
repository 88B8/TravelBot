using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities.Configurations;

namespace TravelBot.Context
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class TravelBotContext : DbContext, IReader, IWriter, IUnitOfWork
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TravelBotContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
        }

        void IWriter.Add<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;


        void IWriter.Delete<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        IQueryable<TEntity> IReader.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);

            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return count;
        }
    }
}
