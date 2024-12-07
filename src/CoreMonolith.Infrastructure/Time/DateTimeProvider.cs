using CoreMonolith.SharedKernel;

namespace CoreMonolith.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
