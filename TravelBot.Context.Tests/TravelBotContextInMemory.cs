using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TravelBot.Context.Contracts;

namespace TravelBot.Context.Tests;

/// <summary>
///     Класс <see cref="TravelBotContext" /> для тестов с базой в памяти. Один контекст на тест
/// </summary>
public abstract class TravelBotContextInMemory : IAsyncDisposable
{
    /// <summary>
    ///     ctor
    /// </summary>
    protected TravelBotContextInMemory()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TravelBotContext>()
            .UseInMemoryDatabase($"TravelBotTests{Guid.NewGuid()}")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        Context = new TravelBotContext(optionsBuilder.Options);
    }

    /// <summary>
    ///     Контекст для <see cref="TravelBotContext" />
    /// </summary>
    protected TravelBotContext Context { get; }

    /// <inheritdoc cref="IUnitOfWork" />
    protected IUnitOfWork UnitOfWork => Context;

    /// <inheritdoc cref="IAsyncDisposable" />
    public async ValueTask DisposeAsync()
    {
        try
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }
        catch (ObjectDisposedException ex)
        {
            Trace.TraceError(ex.Message);
        }
    }

    /// <summary>
    ///     Очищает отслеживаемые сущности из контекста.
    ///     Полезно после Arrange, если тест потом вызывает сервис в том же контексте.
    /// </summary>
    protected void ClearTracking()
    {
        Context.ChangeTracker.Clear();
    }
}