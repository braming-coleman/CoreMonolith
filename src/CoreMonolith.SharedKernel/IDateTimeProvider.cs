namespace CoreMonolith.SharedKernel;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
