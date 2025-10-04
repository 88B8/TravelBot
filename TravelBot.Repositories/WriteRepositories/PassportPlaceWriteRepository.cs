using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IPassportPlaceWriteRepository"/>
    public class PassportPlaceWriteRepository : BaseWriteRepository<PassportPlace>, IPassportPlaceWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PassportPlaceWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}