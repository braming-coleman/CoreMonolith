using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permission_updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[,]
                {
                    { new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), "Read access to [user-permission] resource", "user-permission:read" },
                    { new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), "Write access to [user-permission] resource", "user-permission:write" }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "user_permissions",
                columns: new[] { "id", "permission_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("43b1a7d7-97fb-4b27-be30-a61724e6300c"), new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("610f21e7-77e6-4398-8be2-0330c8111143"), new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("43b1a7d7-97fb-4b27-be30-a61724e6300c"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("610f21e7-77e6-4398-8be2-0330c8111143"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"));
        }
    }
}
