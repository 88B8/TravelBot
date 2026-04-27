using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories;

/// <inheritdoc cref="IAdminReadRepository" />
public class AdminReadRepository : IAdminReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    ///     ctor
    /// </summary>
    public AdminReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<IReadOnlyCollection<Admin>> IAdminReadRepository.GetAll(CancellationToken cancellationToken)
    {
        return reader.Read<Admin>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
    }

    Task<Admin?> IAdminReadRepository.GetById(Guid id, CancellationToken cancellationToken)
    {
        return reader.Read<Admin>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    Task<Admin?> IAdminReadRepository.GetByLogin(string login, CancellationToken cancellationToken)
    {
        return reader.Read<Admin>()
            .NotDeletedAt()
            .Where(x => x.Login == login)
            .FirstOrDefaultAsync(cancellationToken);
    }
}