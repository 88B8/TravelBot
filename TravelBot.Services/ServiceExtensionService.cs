using Microsoft.Extensions.DependencyInjection;
using TravelBot.Common;

namespace TravelBot.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionService
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }
    }
}