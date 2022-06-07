using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class adddeparmentindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291), new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291), new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291), new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291), new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291), new DateTime(2022, 6, 5, 0, 33, 47, 633, DateTimeKind.Utc).AddTicks(8291) });

            migrationBuilder.CreateIndex(
                name: "IX_department_name_enterprise_id_is_soft_deleted",
                table: "department",
                columns: new[] { "name", "enterprise_id", "is_soft_deleted" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_department_name_enterprise_id_is_soft_deleted",
                table: "department");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627), new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627), new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627), new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627), new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627), new DateTime(2022, 6, 3, 22, 2, 57, 294, DateTimeKind.Utc).AddTicks(3627) });
        }
    }
}
