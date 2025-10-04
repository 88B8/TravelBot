using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="ICategoryReadRepository"/>
    public class CategoryReadRepository : ICategoryReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public CategoryReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Category>> ICategoryReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Category>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Category?> ICategoryReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Category>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
