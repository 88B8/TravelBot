using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IRoutePlaceWriteRepository"/>
    public class RoutePlaceWriteRepository : BaseWriteRepository<RoutePlace>, IRoutePlaceWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public RoutePlaceWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}