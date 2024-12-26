using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Application.BusinessLogic.Users;

public sealed record UserResult(User? User, HashSet<string> Permissions);
