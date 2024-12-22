using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permission_weather : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[] { new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89"), "Read access to [weather] resource", "weather:read" });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permission_group_permissions",
                columns: new[] { "id", "permission_group_id", "permission_id" },
                values: new object[,]
                {
                    { new Guid("0193ed22-aa79-7d79-8de1-f4ade9cdfa4d"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89") },
                    { new Guid("0193ed22-aa79-7e19-be3c-504b6c0a244b"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "permission_group_permissions",
                keyColumn: "id",
                keyValue: new Guid("0193ed22-aa79-7d79-8de1-f4ade9cdfa4d"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permission_group_permissions",
                keyColumn: "id",
                keyValue: new Guid("0193ed22-aa79-7e19-be3c-504b6c0a244b"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89"));
        }
    }
}
