using TravelBot.Common.Contracts;

namespace TravelBot.Common.Infrastructure
{
    /// <inheritdoc cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow()
            => DateTimeOffset.UtcNow;
    }
}
