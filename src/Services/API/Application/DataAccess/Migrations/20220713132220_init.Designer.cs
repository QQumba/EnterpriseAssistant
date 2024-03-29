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
    [Migration("20220713132220_init")]
    partial class init
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

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

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

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("ParentDepartmentId");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.HasIndex("Code", "EnterpriseId")
                        .IsUnique();

                    b.ToTable("department");
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

                    b.Property<bool>("DisplayAsMember")
                        .HasColumnType("boolean")
                        .HasColumnName("display_as_member");

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

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Project", b =>
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

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

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

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId", "Name")
                        .IsUnique();

                    b.ToTable("project");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Tasks.Tag", b =>
                {
                    b.Property<long>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("TagId");

                    b.HasIndex("TaskId");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", b =>
                {
                    b.Property<long>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("task_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TaskId"));

                    b.Property<long?>("ClosedByUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("closed_by_user_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("CreatedByUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("created_by_user_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<double?>("EffortHours")
                        .HasColumnType("double precision")
                        .HasColumnName("effort_hours");

                    b.Property<double?>("EstimatedHours")
                        .HasColumnType("double precision")
                        .HasColumnName("estimated_hours");

                    b.Property<bool>("IsTaskGroup")
                        .HasColumnType("boolean")
                        .HasColumnName("is_task_group");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("TaskGroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("task_group_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("TaskId");

                    b.HasIndex("ClosedByUserId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("task");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.TaskUser", b =>
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

                    b.Property<double?>("HoursSpent")
                        .HasColumnType("double precision")
                        .HasColumnName("hours_spent");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_soft_deleted");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("task_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId", "TaskId")
                        .IsUnique();

                    b.ToTable("task_user");
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

                    b.Property<long?>("TaskId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("TaskId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("TaskTask", b =>
                {
                    b.Property<long>("DownstreamTasksTaskId")
                        .HasColumnType("bigint");

                    b.Property<long>("UpstreamTasksTaskId")
                        .HasColumnType("bigint");

                    b.HasKey("DownstreamTasksTaskId", "UpstreamTasksTaskId");

                    b.HasIndex("UpstreamTasksTaskId");

                    b.ToTable("TaskTask");
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

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Project", "Project")
                        .WithOne("Department")
                        .HasForeignKey("EnterpriseAssistant.DataAccess.Entities.Department", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Enterprise");

                    b.Navigation("ParentDepartment");

                    b.Navigation("Project");
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
                        .WithMany("DepartmentUsers")
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
                        .WithMany("EnterpriseUsers")
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

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Project", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Tasks.Tag", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", null)
                        .WithMany("Tags")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "ClosedByUser")
                        .WithMany()
                        .HasForeignKey("ClosedByUserId");

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClosedByUser");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.TaskUser", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.User", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", null)
                        .WithMany("AssignedUsers")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("TaskTask", b =>
                {
                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", null)
                        .WithMany()
                        .HasForeignKey("DownstreamTasksTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", null)
                        .WithMany()
                        .HasForeignKey("UpstreamTasksTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Department", b =>
                {
                    b.Navigation("ChildDepartments");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Enterprise", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Project", b =>
                {
                    b.Navigation("Department");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.Tasks.Task", b =>
                {
                    b.Navigation("AssignedUsers");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("EnterpriseAssistant.DataAccess.Entities.User", b =>
                {
                    b.Navigation("DepartmentUsers");

                    b.Navigation("EnterpriseUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
