﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreMonolith.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreMonolith.Domain.Access.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.HasKey("Id")
                        .HasName("pk_permissions");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasDatabaseName("ix_permissions_key");

                    b.ToTable("permissions", "access");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"),
                            Description = "Read access to [user] resource",
                            Key = "user:read"
                        },
                        new
                        {
                            Id = new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"),
                            Description = "Write access to [user] resource",
                            Key = "user:write"
                        },
                        new
                        {
                            Id = new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"),
                            Description = "Read access to [todo] resource",
                            Key = "todo:read"
                        },
                        new
                        {
                            Id = new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"),
                            Description = "Write access to [todo] resource",
                            Key = "todo:write"
                        },
                        new
                        {
                            Id = new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"),
                            Description = "Read access to [user-permission] resource",
                            Key = "user-permission:read"
                        },
                        new
                        {
                            Id = new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"),
                            Description = "Write access to [user-permission] resource",
                            Key = "user-permission:write"
                        });
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "access");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89"),
                            Email = "test@test.com",
                            FirstName = "Braming",
                            LastName = "Test",
                            PasswordHash = "EC6553E28054BACDE70E7F693DE71E1B7F31AF6963F647B256F8C564DAE41080-9CD8286C7E114D85232224E079FE6E0C"
                        },
                        new
                        {
                            Id = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266"),
                            Email = "service-account@download-manager.com",
                            FirstName = "Download Manager",
                            LastName = "Service Account",
                            PasswordHash = "07BCEA2F74FA1473DFA7AD7262FA1AD768306227F639642F6D1251FF53FC1F56-B8C0BF57037CBBA35D3D9FDCDBC33B6D"
                        });
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.UserPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid")
                        .HasColumnName("permission_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_permissions");

                    b.HasIndex("PermissionId")
                        .HasDatabaseName("ix_user_permissions_permission_id");

                    b.HasIndex("UserId", "PermissionId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_permissions_user_id_permission_id");

                    b.ToTable("user_permissions", "access");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f4bf25be-1bfa-430e-8773-912ac312f2f8"),
                            PermissionId = new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("633afe8b-fd9f-46d7-b94b-9036bdf9e83b"),
                            PermissionId = new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("1cd7916a-aff3-488d-bdc7-3d9de8877d26"),
                            PermissionId = new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("4a6dee70-f862-464f-b643-b90aeea50706"),
                            PermissionId = new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("610f21e7-77e6-4398-8be2-0330c8111143"),
                            PermissionId = new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("43b1a7d7-97fb-4b27-be30-a61724e6300c"),
                            PermissionId = new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"),
                            UserId = new Guid("a057e03a-2d3e-4958-9384-dbc529297d89")
                        },
                        new
                        {
                            Id = new Guid("8fdfbd52-6f5b-40b0-a86b-2624478de3b6"),
                            PermissionId = new Guid("d6cbe40d-3f0b-4402-ba4a-9a2d89536f07"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        },
                        new
                        {
                            Id = new Guid("693c920b-dd14-4654-9c48-c0a6aa0df11c"),
                            PermissionId = new Guid("87f797e2-92cb-4298-8ec8-1a0d0334837b"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        },
                        new
                        {
                            Id = new Guid("1734a74d-14c1-4dc2-9332-4091ba9c7c56"),
                            PermissionId = new Guid("b057d9cd-ce76-4d0b-b79d-d10da140a8e8"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        },
                        new
                        {
                            Id = new Guid("354bdea6-7847-4a21-aeb7-52b39be4719a"),
                            PermissionId = new Guid("d69712b8-195b-4c62-ae66-d4b37702a23d"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        },
                        new
                        {
                            Id = new Guid("5c9b4ceb-0591-484f-8ffd-803deb266c7a"),
                            PermissionId = new Guid("122c9c3d-1ad2-4228-8d0c-53b3a55dcff6"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        },
                        new
                        {
                            Id = new Guid("8bbd1b7e-b91d-46bf-b746-3722376c8186"),
                            PermissionId = new Guid("c9a0b1be-391b-44aa-90d8-aca0757d18d6"),
                            UserId = new Guid("b75e4ad1-0804-427b-abd9-a966e2d12266")
                        });
                });

            modelBuilder.Entity("CoreMonolith.Domain.Todos.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_at");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("due_date");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.PrimitiveCollection<List<string>>("Labels")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("labels");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_todo_items");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_todo_items_user_id");

                    b.ToTable("todo_items", "todo");
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.UserPermission", b =>
                {
                    b.HasOne("CoreMonolith.Domain.Access.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_permissions_permissions_permission_id");

                    b.HasOne("CoreMonolith.Domain.Access.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_permissions_users_user_id");

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoreMonolith.Domain.Todos.TodoItem", b =>
                {
                    b.HasOne("CoreMonolith.Domain.Access.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_todo_items_users_user_id");
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.Permission", b =>
                {
                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.User", b =>
                {
                    b.Navigation("UserPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
