﻿// <auto-generated />
using System;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    [DbContext(typeof(EnterpriseAssistantDbContext))]
    partial class EnterpriseAssistantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("DepartmentType")
                        .HasColumnType("integer")
                        .HasColumnName("department_type");

                    b.Property<string>("EnterpriseId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("enterprise_id");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long?>("ParentDepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_department_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("ParentDepartmentId");

                    b.HasIndex("Name", "EnterpriseId", "IsSoftDeleted")
                        .IsUnique();

                    b.ToTable("department");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            DepartmentType = 0,
                            EnterpriseId = "test",
                            IsSoftDeleted = false,
                            Name = "Test department",
                            UpdatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836)
                        });
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.DepartmentUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<int>("DepartmentUserRole")
                        .HasColumnType("integer")
                        .HasColumnName("department_user_role");

                    b.Property<string>("EnterpriseId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("enterprise_id");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("UserId");

                    b.HasIndex("DepartmentId", "UserId")
                        .IsUnique();

                    b.ToTable("department_user");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            DepartmentId = 1L,
                            DepartmentUserRole = 0,
                            EnterpriseId = "test",
                            IsSoftDeleted = false,
                            UpdatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Enterprise", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("DisplayedName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("displayed_name");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("enterprise");

                    b.HasData(
                        new
                        {
                            Id = "test",
                            CreatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            DisplayedName = "test",
                            IsSoftDeleted = false,
                            UpdatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836)
                        });
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.EnterpriseUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("EnterpriseId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("enterprise_id");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("UserId", "Login", "EnterpriseId")
                        .IsUnique();

                    b.ToTable("enterprise_user");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            EnterpriseId = "test",
                            IsSoftDeleted = false,
                            Login = "test",
                            Role = 0,
                            UpdatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Invite", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("EnterpriseId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("enterprise_id");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("invite_status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_email");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("UserId", "EnterpriseId")
                        .IsUnique();

                    b.ToTable("invite");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836),
                            Email = "test@mail.com",
                            FirstName = "Test",
                            IsSoftDeleted = false,
                            LastName = "User",
                            Password = "qwe",
                            Salt = "test_salt",
                            UpdatedAt = new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836)
                        });
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Department", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Enterprise", "Enterprise")
                        .WithMany("Departments")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Department", "ParentDepartment")
                        .WithMany("ChildDepartments")
                        .HasForeignKey("ParentDepartmentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Enterprise");

                    b.Navigation("ParentDepartment");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.DepartmentUser", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Enterprise", null)
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.EnterpriseUser", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Invite", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Enterprise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Department", b =>
                {
                    b.Navigation("ChildDepartments");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Enterprise", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
