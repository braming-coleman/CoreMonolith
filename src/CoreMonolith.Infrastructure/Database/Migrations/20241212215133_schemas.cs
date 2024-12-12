using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class schemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "todo");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "public",
                newName: "users",
                newSchema: "user");

            migrationBuilder.RenameTable(
                name: "todo_items",
                schema: "public",
                newName: "todo_items",
                newSchema: "todo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "user",
                newName: "users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "todo_items",
                schema: "todo",
                newName: "todo_items",
                newSchema: "public");
        }
    }
}
