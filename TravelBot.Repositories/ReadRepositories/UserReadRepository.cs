using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Extensions;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IUserReadRepository"/>
    public class UserReadRepository : IUserReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// ctor
        /// </summary>
        public UserReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<UserDbModel>> IUserReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .SelectUserDbModel()
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<UserDbModel?> IUserReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .ById(id)
                .SelectUserDbModel()
                .FirstOrDefaultAsync(cancellationToken);

        Task<User?> IUserReadRepository.GetByIdRaw(Guid id, CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<UserDbModel?> IUserReadRepository.GetByTelegramId(long telegramId, CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .Where(x => x.TelegramId == telegramId)
                .SelectUserDbModel()
                .FirstOrDefaultAsync(cancellationToken);
    }
}
