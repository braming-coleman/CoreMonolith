using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modules.UserService.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user_service");

            migrationBuilder.CreateTable(
                name: "permission_groups",
                schema: "user_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "user_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "user_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_group_permissions",
                schema: "user_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_group_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_permission_group_permissions_permission_groups_permission_g",
                        column: x => x.permission_group_id,
                        principalSchema: "user_service",
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_group_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "user_service",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_permission_groups",
                schema: "user_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_group_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permission_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_permission_groups_permission_groups_permission_group_id",
                        column: x => x.permission_group_id,
                        principalSchema: "user_service",
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_permission_groups_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_service",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "user_service",
                table: "permission_groups",
                columns: new[] { "id", "code", "description" },
                values: new object[,]
                {
                    { new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), "admin", "Contains *all* permissions." },
                    { new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), "user", "Contains non-admin permissions." }
                });

            migrationBuilder.InsertData(
                schema: "user_service",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[,]
                {
                    { new Guid("0193eb73-d636-7150-a2bc-13bde0f65734"), "Write access to [permission] resource", "permission:write" },
                    { new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac"), "Read access to [permission] resource", "permission:read" },
                    { new Guid("0193eb73-d636-750a-a839-5571f30fd6b2"), "Read access to [user] resource", "user:read" },
                    { new Guid("0193eb73-d636-770f-9d4d-6f2c6d9ccac3"), "Read access to [permission-group] resource", "permission-group:read" },
                    { new Guid("0193eb73-d636-79e6-b669-87236dbbaa96"), "Write access to [permission-group] resource", "permission-group:write" },
                    { new Guid("0193eb73-d636-7aed-bbbc-963672568d66"), "Write access to [user] resource", "user:write" },
                    { new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89"), "Read access to [weather] resource", "weather:read" },
                    { new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6"), "Access via [api-gateway]", "core-api-gateway-access" },
                    { new Guid("01941328-14ff-7c9a-b8b6-cd2535133d14"), "Read access to [download-client] resource", "download-client:read" },
                    { new Guid("01941328-14ff-7fe3-b305-296f186c6060"), "Write access to [download-client] resource", "download-client:write" }
                });

            migrationBuilder.InsertData(
                schema: "user_service",
                table: "permission_group_permissions",
                columns: new[] { "id", "permission_group_id", "permission_id" },
                values: new object[,]
                {
                    { new Guid("0193ec2f-13a3-70f6-bba2-5e63aaa12173"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-7aed-bbbc-963672568d66") },
                    { new Guid("0193ec2f-13a3-70fd-8d28-2b103e51e8ae"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-7150-a2bc-13bde0f65734") },
                    { new Guid("0193ec2f-13a3-7127-b499-fcef873fa5db"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac") },
                    { new Guid("0193ec2f-13a3-77e6-9b8b-81e474ef00dc"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193eb73-d636-7aed-bbbc-963672568d66") },
                    { new Guid("0193ec2f-13a3-79e7-acd9-861e9bf5d943"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193eb73-d636-750a-a839-5571f30fd6b2") },
                    { new Guid("0193ec2f-13a3-7a5c-83fb-d2b822708e25"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-770f-9d4d-6f2c6d9ccac3") },
                    { new Guid("0193ec2f-13a3-7c0c-af57-eaeab6d451d9"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-750a-a839-5571f30fd6b2") },
                    { new Guid("0193ec2f-13a3-7c6d-a09d-4948b5dc81e2"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-79e6-b669-87236dbbaa96") },
                    { new Guid("0193ec2f-13a3-7c79-adfa-97611eac8222"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac") },
                    { new Guid("0193ed22-aa79-7d79-8de1-f4ade9cdfa4d"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89") },
                    { new Guid("0193ed22-aa79-7e19-be3c-504b6c0a244b"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193ed22-aa79-7cb1-9b74-11345e299d89") },
                    { new Guid("0193f4d5-fba9-7010-bd28-fa71072a4950"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6") },
                    { new Guid("0193f4d5-fba9-733a-a8ff-3b920656c4db"), new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), new Guid("0193f4d3-e613-72e7-8917-a9849ec17bc6") },
                    { new Guid("0194132b-dce6-72c0-a342-41b0571a1602"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("01941328-14ff-7fe3-b305-296f186c6060") },
                    { new Guid("0194132b-dce6-7329-9d00-aa475e0de8ed"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("01941328-14ff-7c9a-b8b6-cd2535133d14") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_permission_group_permissions_permission_group_id",
                schema: "user_service",
                table: "permission_group_permissions",
                column: "permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_group_permissions_permission_id_permission_group",
                schema: "user_service",
                table: "permission_group_permissions",
                columns: new[] { "permission_id", "permission_group_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_groups_code",
                schema: "user_service",
                table: "permission_groups",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_key",
                schema: "user_service",
                table: "permissions",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_permission_groups_permission_group_id",
                schema: "user_service",
                table: "user_permission_groups",
                column: "permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permission_groups_user_id_permission_group_id",
                schema: "user_service",
                table: "user_permission_groups",
                columns: new[] { "user_id", "permission_group_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "user_service",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_external_id",
                schema: "user_service",
                table: "users",
                column: "external_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission_group_permissions",
                schema: "user_service");

            migrationBuilder.DropTable(
                name: "user_permission_groups",
                schema: "user_service");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "user_service");

            migrationBuilder.DropTable(
                name: "permission_groups",
                schema: "user_service");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user_service");
        }
    }
}
