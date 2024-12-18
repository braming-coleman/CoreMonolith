using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class external_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "external_id",
                schema: "access",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"),
                column: "external_id",
                value: new Guid("f286059b-bc48-4172-bb3f-23cac97dcdf6"));

            migrationBuilder.UpdateData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"),
                column: "external_id",
                value: null);

            migrationBuilder.CreateIndex(
                name: "ix_users_external_id",
                schema: "access",
                table: "users",
                column: "external_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_external_id",
                schema: "access",
                table: "users");

            migrationBuilder.DropColumn(
                name: "external_id",
                schema: "access",
                table: "users");
        }
    }
}
