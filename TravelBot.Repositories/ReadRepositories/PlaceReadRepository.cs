using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Extensions;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IPlaceReadRepository"/>
    public class PlaceReadRepository : IPlaceReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public PlaceReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<PlaceDbModel>> IPlaceReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Place>()
                .NotDeletedAt()
                .SelectPlaceDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<PlaceDbModel?> IPlaceReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Place>()
                .NotDeletedAt()
                .ById(id)
                .SelectPlaceDbModel()
                .FirstOrDefaultAsync(cancellationToken);

        Task<Place?> IPlaceReadRepository.GetByIdRaw(Guid id, CancellationToken cancellationToken)
            => reader.Read<Place>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<PlaceDbModel>> IPlaceReadRepository.GetByIds(IReadOnlyCollection<Guid> ids,
            CancellationToken cancellationToken)
            => reader.Read<Place>()
                .ByIds(ids)
                .SelectPlaceDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}