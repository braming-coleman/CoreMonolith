using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.DownloadService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class indempotency_remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "idempotent_requests",
                schema: "download_service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "idempotent_requests",
                schema: "download_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_idempotent_requests", x => x.id);
                });
        }
    }
}
