using TravelBot.Common.Contracts;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IAdminWriteRepository"/> 
    public class AdminWriteRepository : BaseWriteRepository<Admin>, IAdminWriteRepository, IRepositoryAnchor
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AdminWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider) : base(writer, dateTimeProvider)
        {
            
        }
    }
}