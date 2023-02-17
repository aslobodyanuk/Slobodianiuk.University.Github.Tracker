using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommitStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Additions = table.Column<int>(type: "int", nullable: false),
                    Deletions = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GithubUserReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubUserReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SHA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilesCount = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    CommitterId = table.Column<int>(type: "int", nullable: true),
                    StatsId = table.Column<int>(type: "int", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commits_CommitStats_StatsId",
                        column: x => x.StatsId,
                        principalTable: "CommitStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commits_GithubUserReferences_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "GithubUserReferences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commits_GithubUserReferences_CommitterId",
                        column: x => x.CommitterId,
                        principalTable: "GithubUserReferences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commits_AuthorId",
                table: "Commits",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_CommitterId",
                table: "Commits",
                column: "CommitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_StatsId",
                table: "Commits",
                column: "StatsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commits");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "CommitStats");

            migrationBuilder.DropTable(
                name: "GithubUserReferences");
        }
    }
}
