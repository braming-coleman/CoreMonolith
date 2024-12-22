using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

internal class ProcessKeycloakAuthCallbackCommandValidator : AbstractValidator<ProcessKeycloakAuthCallbackCommand>
{
    public ProcessKeycloakAuthCallbackCommandValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Must(email => Email.Create(email).IsSuccess);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
