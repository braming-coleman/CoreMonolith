﻿using CoreMonolith.SharedKernel;

namespace CoreMonolith.Domain.Todos;

public sealed record TodoItemCreatedDomainEvent(Guid TodoItemId) : IDomainEvent;
