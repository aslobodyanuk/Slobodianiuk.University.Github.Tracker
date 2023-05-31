namespace Slobodianiuk.University.Github.Tracker.Database.Views
{
    public static class RepositoryAllTimeStatsView
    {
        public const string Name = "View_RepositoryAllTimeStats";

        public const string CreateView = @$"
IF OBJECT_ID('{Name}', 'V') IS NOT NULL
    DROP VIEW {Name}
GO

CREATE VIEW {Name} AS
SELECT [Date],
    SUM([Additions]) AS Additions,
    SUM([Deletions]) AS Deletions,
    SUM([Total]) AS Total,
    SUM([FilesCount]) AS FilesCount
FROM {RepositoryStatsView.Name}
GROUP BY {RepositoryStatsView.Name}.[Date]
";

        public const string RemoveView = $"DROP VIEW {Name};";
    }
}
