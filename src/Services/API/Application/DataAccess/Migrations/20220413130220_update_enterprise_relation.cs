using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class update_enterprise_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_enterprise_EnterpriseId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "EnterpriseId",
                table: "user",
                newName: "enterprise_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_login_EnterpriseId",
                table: "user",
                newName: "IX_user_login_enterprise_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_EnterpriseId",
                table: "user",
                newName: "IX_user_enterprise_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "enterprise",
                newName: "displayed_name");

            migrationBuilder.RenameColumn(
                name: "department_user_type",
                table: "department_user",
                newName: "department_user_role");

            migrationBuilder.AddColumn<string>(
                name: "owner_email",
                table: "enterprise",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_owner_email",
                table: "enterprise",
                column: "owner_email");

            migrationBuilder.AddForeignKey(
                name: "FK_enterprise_managed_user_owner_email",
                table: "enterprise",
                column: "owner_email",
                principalTable: "managed_user",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_enterprise_enterprise_id",
                table: "user",
                column: "enterprise_id",
                principalTable: "enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enterprise_managed_user_owner_email",
                table: "enterprise");

            migrationBuilder.DropForeignKey(
                name: "FK_user_enterprise_enterprise_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_enterprise_owner_email",
                table: "enterprise");

            migrationBuilder.DropColumn(
                name: "owner_email",
                table: "enterprise");

            migrationBuilder.RenameColumn(
                name: "enterprise_id",
                table: "user",
                newName: "EnterpriseId");

            migrationBuilder.RenameIndex(
                name: "IX_user_login_enterprise_id",
                table: "user",
                newName: "IX_user_login_EnterpriseId");

            migrationBuilder.RenameIndex(
                name: "IX_user_enterprise_id",
                table: "user",
                newName: "IX_user_EnterpriseId");

            migrationBuilder.RenameColumn(
                name: "displayed_name",
                table: "enterprise",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "department_user_role",
                table: "department_user",
                newName: "department_user_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_id",
                table: "user",
                column: "id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_enterprise_EnterpriseId",
                table: "user",
                column: "EnterpriseId",
                principalTable: "enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
