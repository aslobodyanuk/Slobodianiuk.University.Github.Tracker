using Microsoft.Extensions.Options;
using Octokit;
using Slobodianiuk.University.Github.Tracker.Models.Configuration;
using Slobodianiuk.University.Github.Tracker.Models.Github;

namespace Slobodianiuk.University.Github.Tracker.Core.Services
{
    public class GithubService : IGithubService
    {
        private readonly string[] GITHUB_URLS = new string[]
        {
            "https://github.com/",
            "http://github.com/",
            "https://api.github.com/repos/",
            "https://api.github.com/"
        };

        private readonly IOptions<AppConfig> _configuration;
        private GitHubClient _client;

        private GitHubClient Client
        {
            get
            {
                if (_client == null)
                    _client = InitializeClient();

                return _client;
            }
        }

        public GithubService(IOptions<AppConfig> configuration)
        {
            _configuration = configuration;
        }

        private GitHubClient InitializeClient()
        {
            var appName = _configuration?.Value?.GithubClientName;
            var token = _configuration?.Value?.GithubClientToken;

            var client = new GitHubClient(new ProductHeaderValue(appName));

            var tokenAuth = new Credentials(token);
            client.Credentials = tokenAuth;

            return client;
        }

        public async Task<Repository> GetRepository(string url)
        {
            var repoInfo = GetInfoByUrl(url);
            return await Client.Repository.Get(repoInfo.Owner, repoInfo.Repository);
        }

        public async Task<IReadOnlyList<GitHubCommit>> GetCommits(string url)
        {
            var repoInfo = GetInfoByUrl(url);
            return await Client.Repository.Commit.GetAll(repoInfo.Owner, repoInfo.Repository);
        }

        public async Task<GitHubCommit> GetCommitDetails(string url, string sha)
        {
            var repoInfo = GetInfoByUrl(url);
            return await Client.Repository.Commit.Get(repoInfo.Owner, repoInfo.Repository, sha);
        }

        private RepositoryUrlInfo GetInfoByUrl(string url)
        {
            var result = url;

            foreach (var githubUrl in GITHUB_URLS)
                result = result.Replace(githubUrl, string.Empty);

            result = result.Trim('/').Trim('\\');

            var split = result.Split('/');

            if (split.Count() <= 1 || split.Count() > 2)
                throw new Exception($"Invalid Github url '{url}'.");

            return new RepositoryUrlInfo
            {
                Owner = split[0],
                Repository = split[1]
            };
        }
    }
}
