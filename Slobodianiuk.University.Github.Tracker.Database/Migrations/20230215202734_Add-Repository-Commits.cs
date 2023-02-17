using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRepositoryCommits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepositoryId",
                table: "Commits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commits_RepositoryId",
                table: "Commits",
                column: "RepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Repositories_RepositoryId",
                table: "Commits",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Repositories_RepositoryId",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_RepositoryId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "RepositoryId",
                table: "Commits");
        }
    }
}
