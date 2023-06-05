namespace Slobodianiuk.University.Github.Tracker.Database.Views
{
    public static class RepositoryAllTimeStatsView
    {
        public const string Name = "View_RepositoryAllTimeStats";

        public const string CreateView = @$"
IF OBJECT_ID('{Name}', 'V') IS NOT NULL
    DROP VIEW {Name}
GO

SELECT
	[Date],
	SUM([Additions]) AS Additions,
	SUM([Deletions]) AS Deletions,
	SUM([Total]) AS Total,
	SUM([FilesCount]) AS FilesCount
FROM
(
	SELECT
	[Date],
	[FilesCount],
	CASE
		WHEN Additions > 1000 THEN 1000
		ELSE Additions
	END AS Additions,
	CASE
		WHEN Deletions > 1000 THEN 1000
		ELSE Deletions
	END AS Deletions,
	CASE
		WHEN Total > 1000 THEN 1000
		ELSE Total
	END AS Total
	FROM {RepositoryStatsView.Name}
) AS Query
GROUP BY [Query].[Date]
";

        public const string RemoveView = $"DROP VIEW {Name};";
    }
}
