using System.Collections.Concurrent;

namespace TravelBot.Bot.Services;

public sealed class RegistrationStateStore
{
    private readonly ConcurrentDictionary<long, RegistrationState> states = new();

    public void Set(long telegramId, Guid? pendingPlaceId)
    {
        states[telegramId] = new RegistrationState(pendingPlaceId);
    }

    public bool TryGet(long telegramId, out RegistrationState state)
    {
        return states.TryGetValue(telegramId, out state!);
    }

    public void Remove(long telegramId)
    {
        states.TryRemove(telegramId, out _);
    }
}

public sealed record RegistrationState(Guid? PendingPlaceId);