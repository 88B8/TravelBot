using Microsoft.Extensions.DependencyInjection;
using TravelBot.Common;

namespace TravelBot.Repositories.Extensions
{
    /// <summary>
    /// Расширение для <see cref="IServiceCollection"/>
    /// </summary>
    public static class RepositoryExtensionsService
    {
        /// <summary>
        /// Регистрация репозиториев в DI
        /// </summary>
        public static void RegisterRepositories(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
