using FluentValidation;

namespace CoreMonolith.Application.Access.Permissions.Create;

internal sealed class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(c => c.Key).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
