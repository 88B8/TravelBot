using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories;

/// <inheritdoc cref="ICategoryWriteRepository" />
public class CategoryWriteRepository : BaseWriteRepository<Category>, ICategoryWriteRepository, IRepositoryAnchor
{
    /// <summary>
    ///     ctor
    /// </summary>
    public CategoryWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
    {
    }
}