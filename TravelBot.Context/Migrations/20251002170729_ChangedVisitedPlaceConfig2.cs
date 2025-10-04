using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBot.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPassportPlaceConfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlaces_Places_PassportId",
                table: "PassportPlaces");

            migrationBuilder.CreateIndex(
                name: "IX_PassportPlaces_PlaceId",
                table: "PassportPlaces",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlaces_Places_PlaceId",
                table: "PassportPlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlaces_Places_PlaceId",
                table: "PassportPlaces");

            migrationBuilder.DropIndex(
                name: "IX_PassportPlaces_PlaceId",
                table: "PassportPlaces");

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlaces_Places_PassportId",
                table: "PassportPlaces",
                column: "PassportId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
