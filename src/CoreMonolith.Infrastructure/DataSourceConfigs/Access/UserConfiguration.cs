using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreMonolith.Infrastructure.DataSourceConfigs.Access;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", Schemas.Access);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.ExternalId).IsUnique();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasData(
            new User
            {
                Id = Guid.Parse("a057e03a-2d3e-4958-9384-dbc529297d89"),
                ExternalId = Guid.Parse("f286059b-bc48-4172-bb3f-23cac97dcdf6"),
                Email = "test@test.com",
                FirstName = "Braming",
                LastName = "Test",
                PasswordHash = "EC6553E28054BACDE70E7F693DE71E1B7F31AF6963F647B256F8C564DAE41080-9CD8286C7E114D85232224E079FE6E0C"
            },
            new User
            {
                Id = Guid.Parse("b75e4ad1-0804-427b-abd9-a966e2d12266"),
                ExternalId = null,
                Email = "service-account@download-manager.com",
                FirstName = "Download Manager",
                LastName = "Service Account",
                PasswordHash = "07BCEA2F74FA1473DFA7AD7262FA1AD768306227F639642F6D1251FF53FC1F56-B8C0BF57037CBBA35D3D9FDCDBC33B6D"
            });
    }
}
