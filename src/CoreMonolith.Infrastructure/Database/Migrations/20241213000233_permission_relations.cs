using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permission_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_permission_id",
                schema: "access",
                table: "user_permissions",
                column: "permission_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_permissions_permissions_permission_id",
                schema: "access",
                table: "user_permissions",
                column: "permission_id",
                principalSchema: "access",
                principalTable: "permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_permissions_users_user_id",
                schema: "access",
                table: "user_permissions",
                column: "user_id",
                principalSchema: "access",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_permissions_permissions_permission_id",
                schema: "access",
                table: "user_permissions");

            migrationBuilder.DropForeignKey(
                name: "fk_user_permissions_users_user_id",
                schema: "access",
                table: "user_permissions");

            migrationBuilder.DropIndex(
                name: "ix_user_permissions_permission_id",
                schema: "access",
                table: "user_permissions");
        }
    }
}
