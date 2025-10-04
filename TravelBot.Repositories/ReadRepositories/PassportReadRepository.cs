using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Extensions;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IPassportReadRepository"/>
    public class PassportReadRepository : IPassportReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public PassportReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<PassportDbModel>> IPassportReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Passport>()
                .NotDeletedAt()
                .SelectPassportDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<PassportDbModel?> IPassportReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Passport>()
                .NotDeletedAt()
                .ById(id)
                .SelectPassportDbModel()
                .FirstOrDefaultAsync(cancellationToken);

        Task<Passport?> IPassportReadRepository.GetByIdRaw(Guid id, CancellationToken cancellationToken)
            => reader.Read<Passport>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
