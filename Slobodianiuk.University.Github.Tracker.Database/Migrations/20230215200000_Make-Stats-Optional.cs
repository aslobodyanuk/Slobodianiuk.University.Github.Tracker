using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeStatsOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_CommitStats_StatsId",
                table: "Commits");

            migrationBuilder.AlterColumn<int>(
                name: "StatsId",
                table: "Commits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_CommitStats_StatsId",
                table: "Commits",
                column: "StatsId",
                principalTable: "CommitStats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_CommitStats_StatsId",
                table: "Commits");

            migrationBuilder.AlterColumn<int>(
                name: "StatsId",
                table: "Commits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_CommitStats_StatsId",
                table: "Commits",
                column: "StatsId",
                principalTable: "CommitStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
