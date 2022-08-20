using Microsoft.EntityFrameworkCore.Migrations;

namespace Rick_and_Morty.Infrastructure.Persistence.Migrations
{
    public partial class AddedColumnImageNameInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Episodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Characters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Characters");
        }
    }
}
