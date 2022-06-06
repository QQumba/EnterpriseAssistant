using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnterpriseAssistant.DataAccess.Migrations
{
    public partial class add_invites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invites",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    enterprise_id = table.Column<string>(type: "text", nullable: false),
                    invite_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_soft_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invites_enterprise_enterprise_id",
                        column: x => x.enterprise_id,
                        principalTable: "enterprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invites_user_user_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_Invites_enterprise_id",
                table: "Invites",
                column: "enterprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_user_id_enterprise_id",
                table: "Invites",
                columns: new[] { "user_id", "enterprise_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invites");

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
        }
    }
}
