using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class schema_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "access");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "user",
                newName: "users",
                newSchema: "access");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "access",
                newName: "users",
                newSchema: "user");
        }
    }
}
