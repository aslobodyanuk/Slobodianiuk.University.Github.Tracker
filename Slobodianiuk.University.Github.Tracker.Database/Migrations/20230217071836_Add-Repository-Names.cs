using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRepositoryNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Repositories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Repositories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Repositories");
        }
    }
}
