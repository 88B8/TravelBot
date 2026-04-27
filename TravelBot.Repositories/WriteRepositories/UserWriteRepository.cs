using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories;

/// <inheritdoc cref="IUserWriteRepository" />
public class UserWriteRepository : BaseWriteRepository<User>, IUserWriteRepository, IRepositoryAnchor
{
    /// <summary>
    ///     ctor
    /// </summary>
    public UserWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
    {
    }
}