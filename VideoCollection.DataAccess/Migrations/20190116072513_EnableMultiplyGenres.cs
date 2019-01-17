using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoCollection.DataAccess.Migrations
{
    public partial class EnableMultiplyGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
