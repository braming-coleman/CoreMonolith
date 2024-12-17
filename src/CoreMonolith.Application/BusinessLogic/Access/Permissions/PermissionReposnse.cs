namespace CoreMonolith.Application.BusinessLogic.Access.Permissions;

public sealed record PermissionReposnse
{
    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string? Description { get; set; }
}
