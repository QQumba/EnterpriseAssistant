using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class add_user_email_to_invite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                table: "Invites",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "user_email",
                table: "Invites",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705), new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705), new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705), new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705), new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705), new DateTime(2022, 6, 6, 16, 11, 21, 747, DateTimeKind.Utc).AddTicks(6705) });

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "user_email",
                table: "Invites");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                table: "Invites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296), new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296), new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296), new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296), new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296), new DateTime(2022, 6, 6, 15, 40, 5, 261, DateTimeKind.Utc).AddTicks(1296) });

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
