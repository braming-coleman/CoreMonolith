using CoreMonolith.Domain.Access;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.Access;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasData(
            new User
            {
                Id = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"),
                Email = "test@test.com",
                FirstName = "Braming",
                LastName = "Test",
                PasswordHash = "EC6553E28054BACDE70E7F693DE71E1B7F31AF6963F647B256F8C564DAE41080-9CD8286C7E114D85232224E079FE6E0C"
            });
    }
}
