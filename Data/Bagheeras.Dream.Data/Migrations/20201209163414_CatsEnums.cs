using Microsoft.EntityFrameworkCore.Migrations;

namespace Bagheeras.Dream.Data.Migrations
{
    public partial class CatsEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreedId",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreedId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Cats");
        }
    }
}
