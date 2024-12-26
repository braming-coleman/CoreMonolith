namespace CoreMonolith.Domain.Models.Idempotency;

public class IdempotentRequest : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset Created { get; set; }
}
