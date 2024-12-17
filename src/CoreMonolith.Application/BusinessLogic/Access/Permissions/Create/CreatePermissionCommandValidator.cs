using FluentValidation;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;

internal sealed class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(c => c.Key).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
