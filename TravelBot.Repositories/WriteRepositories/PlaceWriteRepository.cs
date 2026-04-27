using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories;

/// <inheritdoc cref="IPlaceWriteRepository" />
public class PlaceWriteRepository : BaseWriteRepository<Place>, IPlaceWriteRepository, IRepositoryAnchor
{
    /// <summary>
    ///     ctor
    /// </summary>
    public PlaceWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
    {
    }
}