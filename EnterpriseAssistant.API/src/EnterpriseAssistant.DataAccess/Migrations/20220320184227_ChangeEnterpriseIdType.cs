using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class ChangeEnterpriseIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "enterprise",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "enterprise_id",
                table: "department",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_department_enterprise_id",
                table: "department",
                column: "enterprise_id");

            migrationBuilder.AddForeignKey(
                name: "FK_department_enterprise_enterprise_id",
                table: "department",
                column: "enterprise_id",
                principalTable: "enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_enterprise_enterprise_id",
                table: "department");

            migrationBuilder.DropIndex(
                name: "IX_department_enterprise_id",
                table: "department");

            migrationBuilder.DropColumn(
                name: "enterprise_id",
                table: "department");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "enterprise",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
