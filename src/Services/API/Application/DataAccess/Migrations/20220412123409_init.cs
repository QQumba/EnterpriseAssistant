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
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "managed_user",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managed_user", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    parent_department_id = table.Column<long>(type: "bigint", nullable: true),
                    department_type = table.Column<int>(type: "integer", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    EnterpriseId = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    managed_user_email = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_user_managed_user_managed_user_email",
                        column: x => x.managed_user_email,
                        principalTable: "managed_user",
                        principalColumn: "email",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "department_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    department_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    department_user_type = table.Column<int>(type: "integer", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
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
                name: "IX_department_enterprise_id",
                table: "department",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_department_parent_department_id",
                table: "department",
                column: "parent_department_id");

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
                name: "IX_managed_user_email",
                table: "managed_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_EnterpriseId",
                table: "user",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_user_id",
                table: "user",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_login_EnterpriseId",
                table: "user",
                columns: new[] { "login", "EnterpriseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_managed_user_email",
                table: "user",
                column: "managed_user_email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department_user");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "enterprise");

            migrationBuilder.DropTable(
                name: "managed_user");
        }
    }
}
