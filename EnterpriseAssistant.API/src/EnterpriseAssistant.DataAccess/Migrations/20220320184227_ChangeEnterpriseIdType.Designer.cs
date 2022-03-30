﻿// <auto-generated />
using System;
using EnterpriseAssistant.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    [DbContext(typeof(EnterpriseAssistantDbContext))]
    [Migration("20220320184227_ChangeEnterpriseIdType")]
    partial class ChangeEnterpriseIdType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("department", (string)null);
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

                    b.Property<int>("DepartmentUserType")
                        .HasColumnType("integer")
                        .HasColumnName("department_user_type");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_login");

                    b.HasKey("Id");

                    b.HasIndex("UserLogin");

                    b.HasIndex("DepartmentId", "UserLogin")
                        .IsUnique();

                    b.ToTable("department_user", (string)null);
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
                        .HasColumnName("name");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("enterprise", (string)null);
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

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

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Login");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("user", (string)null);
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
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

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

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

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
