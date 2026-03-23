using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Account_SchoolId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "rfid",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "school_id",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_school_id",
                table: "accounts",
                column: "school_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_accounts_school_id",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "school_id",
                table: "accounts");

            migrationBuilder.AlterColumn<string>(
                name: "rfid",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
