using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210), new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210), new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210), new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210), new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210), new DateTime(2022, 6, 5, 12, 23, 32, 71, DateTimeKind.Utc).AddTicks(3210) });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714), new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "department_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714), new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "enterprise",
                keyColumn: "id",
                keyValue: "test",
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714), new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "enterprise_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714), new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714), new DateTime(2022, 6, 4, 20, 28, 24, 340, DateTimeKind.Utc).AddTicks(7714) });
        }
    }
}
