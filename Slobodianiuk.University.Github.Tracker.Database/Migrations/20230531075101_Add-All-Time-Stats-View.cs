using Microsoft.EntityFrameworkCore.Migrations;
using Slobodianiuk.University.Github.Tracker.Database.Views;

#nullable disable

namespace Slobodianiuk.University.Github.Tracker.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTimeStatsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(RepositoryAllTimeStatsView.CreateView);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(RepositoryAllTimeStatsView.RemoveView);
        }
    }
}
