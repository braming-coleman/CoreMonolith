using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.DownloadService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class download_client_unique_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_download_clients_type",
                schema: "download_service",
                table: "download_clients",
                column: "type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_download_clients_type",
                schema: "download_service",
                table: "download_clients");
        }
    }
}
