using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class add_display_as_member_to_department_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "display_as_member",
                table: "department_user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265), new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265), new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265), new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265), new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265), new DateTime(2022, 6, 16, 23, 22, 42, 401, DateTimeKind.Utc).AddTicks(3265) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_as_member",
                table: "department_user");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077), new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077), new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077), new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077), new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077), new DateTime(2022, 6, 14, 9, 10, 4, 133, DateTimeKind.Utc).AddTicks(3077) });
        }
    }
}
