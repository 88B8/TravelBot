using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBot.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedManyToManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passport_User_UserId",
                table: "Passport");

            migrationBuilder.DropForeignKey(
                name: "FK_Place_Category_CategoryId",
                table: "Place");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlace_Place_PlaceId",
                table: "RoutePlace");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlace_Route_RouteId",
                table: "RoutePlace");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlace_Passport_PassportId",
                table: "PassportPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlace_Place_PassportId",
                table: "PassportPlace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassportPlace",
                table: "PassportPlace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutePlace",
                table: "RoutePlace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passport",
                table: "Passport");

            migrationBuilder.DropIndex(
                name: "IX_Passport_UserId",
                table: "Passport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Passport");

            migrationBuilder.RenameTable(
                name: "PassportPlace",
                newName: "PassportPlaces");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RoutePlace",
                newName: "RoutePlaces");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.RenameTable(
                name: "Passport",
                newName: "Passports");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_PassportPlace_PassportId",
                table: "PassportPlaces",
                newName: "IX_PassportPlaces_PassportId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlace_RouteId",
                table: "RoutePlaces",
                newName: "IX_RoutePlaces_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlace_PlaceId",
                table: "RoutePlaces",
                newName: "IX_RoutePlaces_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Place_CategoryId",
                table: "Places",
                newName: "IX_Places_CategoryId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "VisitedAt",
                table: "PassportPlaces",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "PassportId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassportPlaces",
                table: "PassportPlaces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutePlaces",
                table: "RoutePlaces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passports",
                table: "Passports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportId",
                table: "Users",
                column: "PassportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Categories_CategoryId",
                table: "Places",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Passports_PassportId",
                table: "Users",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlaces_Passports_PassportId",
                table: "PassportPlaces",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlaces_Places_PassportId",
                table: "PassportPlaces",
                column: "PassportId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Categories_CategoryId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Passports_PassportId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlaces_Passports_PassportId",
                table: "PassportPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportPlaces_Places_PassportId",
                table: "PassportPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassportPlaces",
                table: "PassportPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PassportId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutePlaces",
                table: "RoutePlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passports",
                table: "Passports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "PassportPlaces",
                newName: "PassportPlace");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameTable(
                name: "RoutePlaces",
                newName: "RoutePlace");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.RenameTable(
                name: "Passports",
                newName: "Passport");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_PassportPlaces_PassportId",
                table: "PassportPlace",
                newName: "IX_PassportPlace_PassportId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlaces_RouteId",
                table: "RoutePlace",
                newName: "IX_RoutePlace_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlaces_PlaceId",
                table: "RoutePlace",
                newName: "IX_RoutePlace_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_CategoryId",
                table: "Place",
                newName: "IX_Place_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitedAt",
                table: "PassportPlace",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Passport",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassportPlace",
                table: "PassportPlace",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutePlace",
                table: "RoutePlace",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passport",
                table: "Passport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Passport_UserId",
                table: "Passport",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passport_User_UserId",
                table: "Passport",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Place_Category_CategoryId",
                table: "Place",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlace_Place_PlaceId",
                table: "RoutePlace",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlace_Route_RouteId",
                table: "RoutePlace",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlace_Passport_PassportId",
                table: "PassportPlace",
                column: "PassportId",
                principalTable: "Passport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportPlace_Place_PassportId",
                table: "PassportPlace",
                column: "PassportId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
