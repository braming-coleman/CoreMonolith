﻿using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
