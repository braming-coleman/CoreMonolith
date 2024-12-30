using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.DownloadService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class idempotency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "idempotent_requests",
                schema: "public",
                newName: "idempotent_requests",
                newSchema: "download_service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "idempotent_requests",
                schema: "download_service",
                newName: "idempotent_requests",
                newSchema: "public");
        }
    }
}
