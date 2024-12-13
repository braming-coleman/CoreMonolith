using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class service_account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "access",
                table: "users",
                columns: new[] { "id", "email", "first_name", "last_name", "password_hash" },
                values: new object[] { new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"), "service-account@download-manager.com", "Download Manager", "Service Account", "07BCEA2F74FA1473DFA7AD7262FA1AD768306227F639642F6D1251FF53FC1F56-B8C0BF57037CBBA35D3D9FDCDBC33B6D" });

            migrationBuilder.InsertData(
                schema: "access",
                table: "user_permissions",
                columns: new[] { "id", "permission_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("1734a74d-14c1-4dc2-9332-4091ba9c7c56"), new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("354bdea6-7847-4a21-aeb7-52b39be4719a"), new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("5c9b4ceb-0591-484f-8ffd-803deb266c7a"), new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("693c920b-dd14-4654-9c48-c0a6aa0df11c"), new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("8bbd1b7e-b91d-46bf-b746-3722376c8186"), new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("8fdfbd52-6f5b-40b0-a86b-2624478de3b6"), new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("1734a74d-14c1-4dc2-9332-4091ba9c7c56"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("354bdea6-7847-4a21-aeb7-52b39be4719a"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("5c9b4ceb-0591-484f-8ffd-803deb266c7a"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("693c920b-dd14-4654-9c48-c0a6aa0df11c"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("8bbd1b7e-b91d-46bf-b746-3722376c8186"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "user_permissions",
                keyColumn: "id",
                keyValue: new Guid("8fdfbd52-6f5b-40b0-a86b-2624478de3b6"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"));
        }
    }
}
