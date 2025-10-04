using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IPassportPlaceReadRepository"/>
    public class PassportPlaceReadRepository : IPassportPlaceReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public PassportPlaceReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<PassportPlace>> IPassportPlaceReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<PassportPlace>()
                .NotDeletedAt()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<PassportPlace?> IPassportPlaceReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<PassportPlace>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<PassportPlace>> IPassportPlaceReadRepository.GetByPassportId(Guid id,
            CancellationToken cancellationToken)
            => reader.Read<PassportPlace>()
                .NotDeletedAt()
                .Where(x => x.PassportId == id)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<PassportPlace>> IPassportPlaceReadRepository.GetByPlaceIds(
            IReadOnlyCollection<Guid> ids,
            CancellationToken cancellationToken)
            => reader.Read<PassportPlace>()
                .NotDeletedAt()
                .Where(ByPlaceIds(ids))
                .ToReadOnlyCollectionAsync(cancellationToken);

        /// <summary>
        /// Метод для вычисления идентификатора места
        /// </summary>
        private static Expression<Func<PassportPlace, bool>> ByPlaceIds(IReadOnlyCollection<Guid> ids)
        {
            var quantity = ids.Count;
            return quantity switch
            {
                0 => x => false,
                1 => x => x.PlaceId == ids.First(),
                _ => x => ids.Contains(x.PlaceId)
            };
        }
    }
}
