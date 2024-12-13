using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "access",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                schema: "access",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permissions", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[,]
                {
                    { new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), "Write access to [user] resource", "user:write" },
                    { new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), "Read access to [todo] resource", "todo:read" },
                    { new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), "Write access to [todo] resource", "todo:write" },
                    { new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), "Read access to [user] resource", "user:read" }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "user_permissions",
                columns: new[] { "id", "permission_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("1cd7916a-aff3-488d-bdc7-3d9de8877d26"), new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("4a6dee70-f862-464f-b643-b90aeea50706"), new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("633afe8b-fd9f-46d7-b94b-9036bdf9e83b"), new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("f4bf25be-1bfa-430e-8773-912ac312f2f8"), new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_permissions_key",
                schema: "access",
                table: "permissions",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_user_id_permission_id",
                schema: "access",
                table: "user_permissions",
                columns: new[] { "user_id", "permission_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permissions",
                schema: "access");

            migrationBuilder.DropTable(
                name: "user_permissions",
                schema: "access");
        }
    }
}
