using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IPassportWriteRepository"/>
    public class PassportWriteRepository : BaseWriteRepository<Passport>, IPassportWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PassportWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}