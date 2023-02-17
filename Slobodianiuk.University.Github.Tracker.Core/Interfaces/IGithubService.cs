using Octokit;

namespace Slobodianiuk.University.Github.Tracker.Core.Services
{
    public interface IGithubService
    {
        Task<GitHubCommit> GetCommitDetails(string url, string sha);
        Task<IReadOnlyList<GitHubCommit>> GetCommits(string url);
        Task<Repository> GetRepository(string url);
    }
}