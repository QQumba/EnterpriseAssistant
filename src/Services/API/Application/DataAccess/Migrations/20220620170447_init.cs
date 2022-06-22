using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enterprise",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    displayed_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enterprise_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprise_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_enterprise_user_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enterprise_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invite",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    user_email = table.Column<string>(type: "text", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    invite_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invite", x => x.id);
                    table.ForeignKey(
                        name: "FK_invite_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invite_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    parent_department_id = table.Column<long>(type: "bigint", nullable: true),
                    department_type = table.Column<int>(type: "integer", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    project_id = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_department_parent_department_id",
                        column: x => x.parent_department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_department_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_department_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "department_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    department_user_role = table.Column<int>(type: "integer", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    display_as_member = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_user_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_department_user_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_department_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_code_enterprise_id",
                table: "department",
                columns: new[] { "code", "enterprise_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_enterprise_id",
                table: "department",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_parent_department_id",
                table: "department",
                column: "parent_department_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_project_id",
                table: "department",
                column: "project_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_user_department_id_user_id",
                table: "department_user",
                columns: new[] { "department_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_user_enterprise_id",
                table: "department_user",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_user_user_id",
                table: "department_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_id",
                table: "enterprise",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_user_enterprise_id",
                table: "enterprise_user",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_user_user_id_login_enterprise_id",
                table: "enterprise_user",
                columns: new[] { "user_id", "login", "enterprise_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invite_enterprise_id",
                table: "invite",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_invite_user_id_enterprise_id",
                table: "invite",
                columns: new[] { "user_id", "enterprise_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_enterprise_id_name",
                table: "project",
                columns: new[] { "enterprise_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_task_enterprise_id",
                table: "task",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_project_id",
                table: "task",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_user_id",
                table: "task",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department_user");

            migrationBuilder.DropTable(
                name: "enterprise_user");

            migrationBuilder.DropTable(
                name: "invite");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "enterprise");
        }
    }
}
