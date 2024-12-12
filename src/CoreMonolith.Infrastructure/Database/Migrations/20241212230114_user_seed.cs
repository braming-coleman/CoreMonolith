using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class user_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "access",
                table: "users",
                columns: new[] { "id", "email", "first_name", "last_name", "password_hash" },
                values: new object[] { new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"), "test@test.com", "Braming", "Test", "EC6553E28054BACDE70E7F693DE71E1B7F31AF6963F647B256F8C564DAE41080-9CD8286C7E114D85232224E079FE6E0C" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"));
        }
    }
}
