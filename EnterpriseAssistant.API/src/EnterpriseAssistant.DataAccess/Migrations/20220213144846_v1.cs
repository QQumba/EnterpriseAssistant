using System;
using EnterpriseAssistant.DataAccess.Sql.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_user_user_user_id",
                table: "department_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_department_user_department_id_user_id",
                table: "department_user");

            migrationBuilder.DropIndex(
                name: "IX_department_user_user_id",
                table: "department_user");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "department_user");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "user",
                newName: "salt");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department_user_type",
                table: "department_user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "user_login",
                table: "department_user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "department_type",
                table: "department",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "login");

            migrationBuilder.CreateTable(
                name: "enterprise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprise", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_user_department_id_user_login",
                table: "department_user",
                columns: new[] { "department_id", "user_login" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_user_user_login",
                table: "department_user",
                column: "user_login");

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_id",
                table: "enterprise",
                column: "id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_department_user_user_user_login",
                table: "department_user",
                column: "user_login",
                principalTable: "user",
                principalColumn: "login",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.CreateSqlRoutinesFromAssembly(GetType().Assembly, SqlRoutines.Function);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_user_user_user_login",
                table: "department_user");

            migrationBuilder.DropTable(
                name: "enterprise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_department_user_department_id_user_login",
                table: "department_user");

            migrationBuilder.DropIndex(
                name: "IX_department_user_user_login",
                table: "department_user");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "password",
                table: "user");

            migrationBuilder.DropColumn(
                name: "role",
                table: "user");

            migrationBuilder.DropColumn(
                name: "department_user_type",
                table: "department_user");

            migrationBuilder.DropColumn(
                name: "user_login",
                table: "department_user");

            migrationBuilder.DropColumn(
                name: "department_type",
                table: "department");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "user",
                newName: "name");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "department_user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_department_user_department_id_user_id",
                table: "department_user",
                columns: new[] { "department_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_user_user_id",
                table: "department_user",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_department_user_user_user_id",
                table: "department_user",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
