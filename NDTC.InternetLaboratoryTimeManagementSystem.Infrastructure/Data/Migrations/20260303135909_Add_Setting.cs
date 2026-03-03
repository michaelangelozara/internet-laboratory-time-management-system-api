using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Setting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "now()"),
                    last_modified_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    is_syncing = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_settings_created_at",
                table: "settings",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_settings_last_modified_at",
                table: "settings",
                column: "last_modified_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings");
        }
    }
}
