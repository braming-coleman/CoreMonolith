using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class gateway_permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[] { new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6"), "Access via [api-gateway]", "core-api-gateway-access" });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permission_group_permissions",
                columns: new[] { "id", "permission_group_id", "permission_id" },
                values: new object[,]
                {
                    { new Guid("0193f4d5-fba9-7010-bd28-fa71072a4950"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6") },
                    { new Guid("0193f4d5-fba9-733a-a8ff-3b920656c4db"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "permission_group_permissions",
                keyColumn: "id",
                keyValue: new Guid("0193f4d5-fba9-7010-bd28-fa71072a4950"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permission_group_permissions",
                keyColumn: "id",
                keyValue: new Guid("0193f4d5-fba9-733a-a8ff-3b920656c4db"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6"));
        }
    }
}
