using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBot.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PassportId",
                table: "Users");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportId",
                table: "Users",
                column: "PassportId",
                unique: true);
        }
    }
}
