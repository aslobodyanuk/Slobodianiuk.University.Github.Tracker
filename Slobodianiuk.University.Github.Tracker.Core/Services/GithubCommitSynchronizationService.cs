using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;
using Slobodianiuk.University.Github.Tracker.Database;
using Slobodianiuk.University.Github.Tracker.Models.Configuration;
using Commit = Slobodianiuk.University.Github.Tracker.Models.Database.Commit;
using Repository = Slobodianiuk.University.Github.Tracker.Models.Database.Repository;

namespace Slobodianiuk.University.Github.Tracker.Core.Services
{
    public class GithubCommitSynchronizationService : IGithubCommitSynchronizationService
    {
        private readonly IMapper _mapper;
        private readonly IGithubService _githubService;
        private readonly TrackerDbContext _dbContext;
        private readonly IOptions<AppConfig> _configuration;
        private readonly ILogger<GithubCommitSynchronizationService> _logger;

        public GithubCommitSynchronizationService(
            IMapper mapper, 
            IGithubService githubService, 
            TrackerDbContext dbContext, 
            IOptions<AppConfig> configuration,
            ILogger<GithubCommitSynchronizationService> logger)
        {
            _mapper = mapper;
            _githubService = githubService;
            _dbContext = dbContext;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task ExecuteSynchronization()
        {
            _logger.LogInformation("Starting commit synchronization.");

            var repositories = await _dbContext.Repositories.ToListAsync();

            foreach (var repository in repositories)
            {
                try
                {
                    await LoadRepositoryAndCommitsIntoDatabase(repository);
                }
                catch (ApiException exc)
                {
                    _logger.LogError(exc, $"Error occured while synchronizing repository with url '{repository?.Url}'.");
                }
            }

            _logger.LogInformation("Commit synchronization complete.");
        }

        private async Task LoadRepositoryAndCommitsIntoDatabase(Repository repository)
        {
            var updatedRepository = await LoadRepository(repository);

            var commits = await _githubService.GetCommits(updatedRepository.Url);
            var existingCommitIds = repository?.Commits?.Select(x => x.SHA) ?? Enumerable.Empty<string>();

            var commitsToLoad = commits.Where(x => existingCommitIds.Contains(x.Sha) == false);

            var commitsForDatabase = await LoadAllCommits(updatedRepository, commitsToLoad);

            updatedRepository.Commits ??= new List<Commit>();

            foreach (var commit in commitsForDatabase)
                updatedRepository.Commits.Add(commit);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Saved '{commitsForDatabase?.Count() ?? 0}' commits to database from repository '{updatedRepository?.Url}'.");
        }

        private async Task<IEnumerable<Commit>> LoadAllCommits(Repository repository, IEnumerable<Octokit.GitHubCommit> commits)
        {
            var delayMs = _configuration?.Value?.CommitFetchDelayMS ?? 10;
            var results = new List<Commit>();

            foreach (var commit in commits)
            {
                var loaded = await LoadCommit(repository, commit);
                results.Add(loaded);

                await Task.Delay(delayMs);
            }

            return results;
        }

        private async Task<Commit> LoadCommit(Repository repository, Octokit.GitHubCommit commit)
        {
            _logger.LogInformation($"Loading commit for repository '{repository.Url}' - '{commit.Sha}'.");

            var loadedCommit = await _githubService.GetCommitDetails(repository.Url, commit.Sha);
            return _mapper.Map<Commit>(loadedCommit);
        }

        private async Task<Repository> LoadRepository(Repository repository)
        {
            if (string.IsNullOrWhiteSpace(repository.Json) == false)
                return repository;

            _logger.LogInformation($"Loading info for repository '{repository.Url}'.");

            var url = RemoveGitFromUrl(repository.Url);
            var apiRepository = await _githubService.GetRepository(url);
            var mapped = _mapper.Map(apiRepository, repository);

            mapped.Url = url;

            _dbContext.Update(mapped);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Repository '{repository.Url}' saved to database.");

            return mapped;
        }

        private string RemoveGitFromUrl(string url)
        {
            var position = url.LastIndexOf(".git");
            if (position >= 0)
                url = url.Substring(0, position);

            return url;
        }
    }
}
