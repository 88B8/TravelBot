using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Extensions;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IRouteReadRepository"/>
    public class RouteReadRepository : IRouteReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public RouteReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<RouteDbModel>> IRouteReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Route>()
                .NotDeletedAt()
                .SelectRouteDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<RouteDbModel?> IRouteReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Route>()
                .NotDeletedAt()
                .ById(id)
                .SelectRouteDbModel()
                .FirstOrDefaultAsync(cancellationToken);

        Task<Route?> IRouteReadRepository.GetByIdRaw(Guid id, CancellationToken cancellationToken)
            => reader.Read<Route>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
