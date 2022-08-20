using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rick_and_Morty.Infrastructure.Persistence.Migrations
{
    public partial class AddedColumnsInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlaceOfBirthId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlaceOfBirthId",
                table: "Characters",
                column: "PlaceOfBirthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_PlaceOfBirthId",
                table: "Characters",
                column: "PlaceOfBirthId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_PlaceOfBirthId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PlaceOfBirthId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirthId",
                table: "Characters");
        }
    }
}
