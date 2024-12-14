using CoreMonolith.Domain.Access;
using CoreMonolith.Domain.Todos;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Todos;

internal sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("todo_items", Schemas.Todo);

        builder.HasKey(t => t.Id);

        builder.HasOne<User>().WithMany().HasForeignKey(t => t.UserId);
    }
}
