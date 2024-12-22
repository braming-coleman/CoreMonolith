namespace CoreMonolith.SharedKernel.Errors;

public static class EmailError
{
    public static Error FormatFailure(string value) => new(
        "Email.FormatFailure",
        $"Invalid email address, value: '{value}'.",
        ErrorType.FormatFailure);
}
