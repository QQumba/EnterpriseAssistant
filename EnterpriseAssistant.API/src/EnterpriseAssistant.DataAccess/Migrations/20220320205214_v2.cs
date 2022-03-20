using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "enterprise_id",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_enterprise_id",
                table: "user",
                column: "enterprise_id");

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
                name: "FK_user_enterprise_enterprise_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_enterprise_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "enterprise_id",
                table: "user");
        }
    }
}
