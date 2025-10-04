using Microsoft.Extensions.DependencyInjection;
using TravelBot.Context.Contracts;

namespace TravelBot.Context
{
    /// <summary>
    /// Расширение для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ContextExtensionsService
    {
        /// <summary>
        /// Регистрирует контекст
        /// </summary>
        public static void RegisterContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReader>(x => x.GetRequiredService<TravelBotContext>());
            serviceCollection.AddScoped<IWriter>(x => x.GetRequiredService<TravelBotContext>());
            serviceCollection.AddScoped<IUnitOfWork>(x => x.GetRequiredService<TravelBotContext>());
        }
    }
}
