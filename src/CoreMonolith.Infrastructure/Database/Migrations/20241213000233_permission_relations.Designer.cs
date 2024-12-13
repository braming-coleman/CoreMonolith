﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CoreMonolith.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreMonolith.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241213000233_permission_relations")]
    partial class permission_relations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .IsRequired()
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
                        });
                });

            modelBuilder.Entity("CoreMonolith.Domain.Access.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
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
                        .IsRequired()
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
                    b.HasOne("CoreMonolith.Domain.Access.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_permissions_permissions_permission_id");

                    b.HasOne("CoreMonolith.Domain.Access.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_permissions_users_user_id");
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
#pragma warning restore 612, 618
        }
    }
}
