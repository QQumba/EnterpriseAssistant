using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class change_invite_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_enterprise_enterprise_id",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invites",
                table: "Invites");

            migrationBuilder.RenameTable(
                name: "Invites",
                newName: "invite");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_user_id_enterprise_id",
                table: "invite",
                newName: "IX_invite_user_id_enterprise_id");

            migrationBuilder.RenameIndex(
                name: "IX_Invites_enterprise_id",
                table: "invite",
                newName: "IX_invite_enterprise_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_invite",
                table: "invite",
                column: "id");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836), new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836), new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836), new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836), new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836), new DateTime(2022, 6, 6, 16, 12, 34, 681, DateTimeKind.Utc).AddTicks(3836) });

            migrationBuilder.AddForeignKey(
                name: "FK_invite_enterprise_enterprise_id",
                table: "invite",
                column: "enterprise_id",
                principalTable: "enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invite_user_user_id",
                table: "invite",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invite_enterprise_enterprise_id",
                table: "invite");

            migrationBuilder.DropForeignKey(
                name: "FK_invite_user_user_id",
                table: "invite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invite",
                table: "invite");

            migrationBuilder.RenameTable(
                name: "invite",
                newName: "Invites");

            migrationBuilder.RenameIndex(
                name: "IX_invite_user_id_enterprise_id",
                table: "Invites",
                newName: "IX_Invites_user_id_enterprise_id");

            migrationBuilder.RenameIndex(
                name: "IX_invite_enterprise_id",
                table: "Invites",
                newName: "IX_Invites_enterprise_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invites",
                table: "Invites",
                column: "id");

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
                name: "FK_Invites_enterprise_enterprise_id",
                table: "Invites",
                column: "enterprise_id",
                principalTable: "enterprise",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_user_user_id",
                table: "Invites",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
