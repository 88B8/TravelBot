using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBot.Context.Migrations
{
    /// <inheritdoc />
    public partial class PassportConfigChanged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PassportId",
                table: "Users");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportId",
                table: "Users",
                column: "PassportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PassportId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Passports");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportId",
                table: "Users",
                column: "PassportId",
                unique: true);
        }
    }
}
