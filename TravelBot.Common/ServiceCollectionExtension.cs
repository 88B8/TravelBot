using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TravelBot.Common;

/// <summary>
///     Расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///     Регистрация с помощью маркерных интерфейсов
    /// </summary>
    public static void RegistrationOnInterface<TInterface>(this IServiceCollection serviceDescriptors,
        ServiceLifetime lifetime)
    {
        var serviceType = typeof(TInterface);
        var allTypes = serviceType.Assembly.GetTypes()
            .Where(x => serviceType.IsAssignableFrom(x)
                        && !(x.IsInterface || x.IsAbstract));

        foreach (var type in allTypes)
        {
            serviceDescriptors.TryAdd(new ServiceDescriptor(type, type, lifetime));
            var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(x => x != typeof(IDisposable) && x.IsPublic && x != serviceType);

            foreach (var interfaceType in interfaces)
                serviceDescriptors.TryAdd(new ServiceDescriptor(interfaceType, provider =>
                    provider.GetRequiredService(type), lifetime));
        }
    }
}