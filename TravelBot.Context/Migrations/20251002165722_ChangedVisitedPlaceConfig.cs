using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBot.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPassportPlaceConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PassportId1",
                table: "PassportPlaces",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassportPlaces_PassportId1",
                table: "PassportPlaces",
                column: "PassportId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlaces_Passports_PassportId1",
                table: "PassportPlaces",
                column: "PassportId1",
                principalTable: "Passports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlaces_Passports_PassportId1",
                table: "PassportPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PassportPlaces_PassportId1",
                table: "PassportPlaces");

            migrationBuilder.DropColumn(
                name: "PassportId1",
                table: "PassportPlaces");
        }
    }
}
