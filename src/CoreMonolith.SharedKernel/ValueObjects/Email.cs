using CoreMonolith.SharedKernel.Errors;
using System.Net.Mail;

namespace CoreMonolith.SharedKernel.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrEmpty(email))
            return Result<Email>.ValidationFailure(Error.ArgumentNull(nameof(email)));

        if (!IsValidEmailFormat(email))
            return Result<Email>.ValidationFailure(EmailError.FormatFailure(email));

        return Result<Email>.Success(new Email(email));
    }

    private static bool IsValidEmailFormat(string email)
    {
        // Use a regular expression or a dedicated library to validate the email format
        // This is a simple example, consider using a more robust validation method
        return MailAddress.TryCreate(email, out _);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}