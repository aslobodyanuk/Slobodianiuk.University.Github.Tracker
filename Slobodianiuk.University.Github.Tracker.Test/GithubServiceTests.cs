using Slobodianiuk.University.Github.Tracker.Core.Services;
using Slobodianiuk.University.Github.Tracker.Core.Services.Frontend;

namespace Slobodianiuk.University.Github.Tracker.Test
{
    [TestClass]
    public class GithubServiceTests : TestBase
    {
        ITrackerFrontendService _trackerFrontendService;
        IGithubCommitSynchronizationService _githubCommitSynchronizationService;
        IGithubService _githubService;

        public GithubServiceTests()
        {
            _trackerFrontendService = ResolveService<ITrackerFrontendService>();
            _githubCommitSynchronizationService = ResolveService<IGithubCommitSynchronizationService>();
            _githubService = ResolveService<IGithubService>();
        }

        [TestMethod]
        public async Task LoadGithubRepository()
        {
            var repository = await _githubService.GetRepository("https://github.com/aslobodyanuk/HelpDeskTeamProject");
        }

        [TestMethod]
        public async Task ExecuteSynchronization()
        {
            await _githubCommitSynchronizationService.ExecuteSynchronization();
        }

        [TestMethod]
        public async Task LoadAllData()
        {
            var result = await _trackerFrontendService.LoadAllData(DateTime.Parse("2018-04-18"), DateTime.Parse("2018-04-21"));
        }
    }
}