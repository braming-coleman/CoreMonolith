using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permission_listing : Migration
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
                    { new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), "Write access to [permission] resource", "permission:write" },
                    { new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), "Read access to [permission] resource", "permission:read" }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "user_permissions",
                columns: new[] { "id", "permission_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("03183e06-4c68-42a0-9752-a529a12b1936"), new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("468aec95-abc6-4522-a406-344c99ccac58"), new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("b6d32088-4628-49ea-a2d3-01ba347f39a2"), new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("c6373e0a-dae5-4c50-96ee-a785b8483a48"), new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("03183e06-4c68-42a0-9752-a529a12b1936"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("468aec95-abc6-4522-a406-344c99ccac58"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("b6d32088-4628-49ea-a2d3-01ba347f39a2"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("c6373e0a-dae5-4c50-96ee-a785b8483a48"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"));
        }
    }
}
