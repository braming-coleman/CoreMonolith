using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class permission_overhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_permissions",
                schema: "access");

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"));

            migrationBuilder.DropColumn(
                name: "password_hash",
                schema: "access",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "access",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "access",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "access",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "key",
                schema: "access",
                table: "permissions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "access",
                table: "permissions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "permission_groups",
                schema: "access",
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
                name: "permission_group_permissions",
                schema: "access",
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
                        principalSchema: "access",
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_group_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "access",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_permission_groups",
                schema: "access",
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
                        principalSchema: "access",
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_permission_groups_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "access",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permission_groups",
                columns: new[] { "id", "code", "description" },
                values: new object[,]
                {
                    { new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), "admin", "Contains *all* permissions." },
                    { new Guid("0193ec1f-35c9-747a-91f1-5601ca02c36f"), "user", "Contains non-admin permissions." }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[,]
                {
                    { new Guid("0193eb73-d636-7150-a2bc-13bde0f65734"), "Write access to [permission] resource", "permission:write" },
                    { new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac"), "Read access to [permission] resource", "permission:read" },
                    { new Guid("0193eb73-d636-750a-a839-5571f30fd6b2"), "Read access to [user] resource", "user:read" },
                    { new Guid("0193eb73-d636-770f-9d4d-6f2c6d9ccac3"), "Read access to [permission-group] resource", "permission-group:read" },
                    { new Guid("0193eb73-d636-79e6-b669-87236dbbaa96"), "Write access to [permission-group] resource", "permission-group:write" },
                    { new Guid("0193eb73-d636-7aed-bbbc-963672568d66"), "Write access to [user] resource", "user:write" }
                });

            migrationBuilder.InsertData(
                schema: "access",
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
                    { new Guid("0193ec2f-13a3-7c79-adfa-97611eac8222"), new Guid("0193ec1f-35c9-723c-a203-67c5e4e0eb75"), new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_permission_group_permissions_permission_group_id",
                schema: "access",
                table: "permission_group_permissions",
                column: "permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_group_permissions_permission_id_permission_group",
                schema: "access",
                table: "permission_group_permissions",
                columns: new[] { "permission_id", "permission_group_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_groups_code",
                schema: "access",
                table: "permission_groups",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_permission_groups_permission_group_id",
                schema: "access",
                table: "user_permission_groups",
                column: "permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permission_groups_user_id_permission_group_id",
                schema: "access",
                table: "user_permission_groups",
                columns: new[] { "user_id", "permission_group_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission_group_permissions",
                schema: "access");

            migrationBuilder.DropTable(
                name: "user_permission_groups",
                schema: "access");

            migrationBuilder.DropTable(
                name: "permission_groups",
                schema: "access");

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-7150-a2bc-13bde0f65734"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-72c1-b3fb-52c82f3593ac"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-750a-a839-5571f30fd6b2"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-770f-9d4d-6f2c6d9ccac3"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-79e6-b669-87236dbbaa96"));

            migrationBuilder.DeleteData(
                schema: "access",
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0193eb73-d636-7aed-bbbc-963672568d66"));

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "access",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "access",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "access",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                schema: "access",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "key",
                schema: "access",
                table: "permissions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "access",
                table: "permissions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "user_permissions",
                schema: "access",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "access",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_permissions_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "access",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "permissions",
                columns: new[] { "id", "description", "key" },
                values: new object[,]
                {
                    { new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), "Read access to [user-permission] resource", "user-permission:read" },
                    { new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), "Write access to [permission] resource", "permission:write" },
                    { new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), "Read access to [permission] resource", "permission:read" },
                    { new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), "Write access to [user] resource", "user:write" },
                    { new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), "Read access to [todo] resource", "todo:read" },
                    { new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), "Write access to [user-permission] resource", "user-permission:write" },
                    { new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), "Write access to [todo] resource", "todo:write" },
                    { new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), "Read access to [user] resource", "user:read" }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "users",
                columns: new[] { "id", "email", "external_id", "first_name", "last_name", "password_hash" },
                values: new object[,]
                {
                    { new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"), "test@test.com", new Guid("f286059b-bc48-4172-bb3f-23cac97dcdf6"), "Braming", "Test", "EC6553E28054BACDE70E7F693DE71E1B7F31AF6963F647B256F8C564DAE41080-9CD8286C7E114D85232224E079FE6E0C" },
                    { new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"), "service-account@download-manager.com", null, "Download Manager", "Service Account", "07BCEA2F74FA1473DFA7AD7262FA1AD768306227F639642F6D1251FF53FC1F56-B8C0BF57037CBBA35D3D9FDCDBC33B6D" }
                });

            migrationBuilder.InsertData(
                schema: "access",
                table: "user_permissions",
                columns: new[] { "id", "permission_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("03183e06-4c68-42a0-9752-a529a12b1936"), new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("1734a74d-14c1-4dc2-9332-4091ba9c7c56"), new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("1cd7916a-aff3-488d-bdc7-3d9de8877d26"), new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("354bdea6-7847-4a21-aeb7-52b39be4719a"), new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("43b1a7d7-97fb-4b27-be30-a61724e6300c"), new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("468aec95-abc6-4522-a406-344c99ccac58"), new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("4a6dee70-f862-464f-b643-b90aeea50706"), new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("5c9b4ceb-0591-484f-8ffd-803deb266c7a"), new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("610f21e7-77e6-4398-8be2-0330c8111143"), new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("633afe8b-fd9f-46d7-b94b-9036bdf9e83b"), new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") },
                    { new Guid("693c920b-dd14-4654-9c48-c0a6aa0df11c"), new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("8bbd1b7e-b91d-46bf-b746-3722376c8186"), new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("8fdfbd52-6f5b-40b0-a86b-2624478de3b6"), new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("b6d32088-4628-49ea-a2d3-01ba347f39a2"), new Guid("82fddd77-f662-4927-aae1-46ebb00c2c05"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("c6373e0a-dae5-4c50-96ee-a785b8483a48"), new Guid("86c9629c-c5e5-49ad-94b7-6981f921386b"), new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266") },
                    { new Guid("f4bf25be-1bfa-430e-8773-912ac312f2f8"), new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"), new Guid("a057e03a-2d3e-4958-9384-dbc529297d89") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_permission_id",
                schema: "access",
                table: "user_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_permissions_user_id_permission_id",
                schema: "access",
                table: "user_permissions",
                columns: new[] { "user_id", "permission_id" },
                unique: true);
        }
    }
}
