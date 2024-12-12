using CoreMonolith.SharedKernel.Abstractions;

namespace CoreMonolith.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
