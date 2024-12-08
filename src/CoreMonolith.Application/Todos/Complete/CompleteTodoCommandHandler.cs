using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Todos;
using CoreMonolith.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Todos.Complete;

internal sealed class CompleteTodoCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CompleteTodoCommand>
{
    public async Task<Result> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await context.TodoItems
            .SingleOrDefaultAsync(t => t.Id == command.TodoItemId, cancellationToken);

        if (todoItem == null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
        }

        // TODO: What if it's already completed? Throw an exception? Return a failure?
        todoItem.IsCompleted = true;
        todoItem.CompletedAt = dateTimeProvider.UtcNow;

        todoItem.Raise(new TodoItemCompletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
