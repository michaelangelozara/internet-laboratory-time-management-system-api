using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client_devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    connection_id = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    connected_at = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_devices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_devices_connection_id",
                table: "client_devices",
                column: "connection_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_devices_name",
                table: "client_devices",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_devices");
        }
    }
}
