namespace Slobodianiuk.University.Github.Tracker.Database.Views
{
    public static class RepositoryStatsView
    {
        public const string Name = "View_RepositoryStats";

        public const string CreateView = @$"
IF OBJECT_ID('{Name}', 'V') IS NOT NULL
    DROP VIEW {Name}
GO

CREATE VIEW {Name} AS
SELECT 
	Repositories.[Name], 
	Repositories.[Surname], 
	Repositories.[Url],
	Repositories.[AltName],
	[Stats].*
FROM (
		
	SELECT 
		Repositories.[Id] AS RepositoryId,
		CAST([GithubUserReferences].[Date] AS DATE) AS [Date],
		SUM(CommitStats.Additions) AS Additions,
		SUM(CommitStats.Deletions) AS Deletions,
		SUM(CommitStats.Total) AS Total,
		SUM(Commits.FilesCount) AS FilesCount
	FROM Repositories
	INNER JOIN Commits on Commits.RepositoryId = Repositories.Id
	INNER JOIN CommitStats on CommitStats.Id = Commits.StatsId
	INNER JOIN GithubUserReferences on GithubUserReferences.Id = Commits.AuthorId
	GROUP BY Repositories.Id, CAST([GithubUserReferences].[Date] AS DATE)

) AS [Stats]
INNER JOIN Repositories ON [Stats].RepositoryId = Repositories.Id;
";

        public const string RemoveView = $"DROP VIEW {Name};";
    }
}
