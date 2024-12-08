using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Todos;
using CoreMonolith.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Todos.GetById;

internal sealed class GetTodoByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTodoByIdQuery, TodoResponse>
{
    public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        TodoResponse todo = await context.TodoItems
            .Where(todoItem => todoItem.Id == query.TodoItemId)
            .Select(todoItem => new TodoResponse
            {
                Id = todoItem.Id,
                UserId = todoItem.UserId,
                Description = todoItem.Description,
                DueDate = todoItem.DueDate,
                Labels = todoItem.Labels,
                IsCompleted = todoItem.IsCompleted,
                CreatedAt = todoItem.CreatedAt,
                CompletedAt = todoItem.CompletedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (todo is null)
        {
            return Result.Failure<TodoResponse>(TodoItemErrors.NotFound(query.TodoItemId));
        }

        return todo;
    }
}
