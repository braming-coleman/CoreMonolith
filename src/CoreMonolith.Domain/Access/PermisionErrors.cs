using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Access;

public static class PermisionErrors
{
    public static Error NotFound(Guid Id) => Error.NotFound(
        "Permision.NotFound",
        $"The permision with the Id = '{Id}' was not found.");
}
