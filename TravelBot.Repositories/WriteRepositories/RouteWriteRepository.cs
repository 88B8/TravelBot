using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories;

/// <inheritdoc cref="IRouteWriteRepository" />
public class RouteWriteRepository : BaseWriteRepository<Route>, IRouteWriteRepository, IRepositoryAnchor
{
    /// <summary>
    ///     ctor
    /// </summary>
    public RouteWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
    {
    }
}