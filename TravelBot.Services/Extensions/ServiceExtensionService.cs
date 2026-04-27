using Microsoft.Extensions.DependencyInjection;
using TravelBot.Common;
using TravelBot.Services.Anchors;

namespace TravelBot.Services.Extensions;

/// <summary>
///     Расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceExtensionService
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
    }
}