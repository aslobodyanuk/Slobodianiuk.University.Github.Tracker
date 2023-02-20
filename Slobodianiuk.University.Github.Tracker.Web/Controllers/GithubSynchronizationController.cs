using Microsoft.AspNetCore.Mvc;
using Slobodianiuk.University.Github.Tracker.Core.Services;

namespace Slobodianiuk.University.Github.Tracker.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GithubSynchronizationController : ControllerBase
    {
        private readonly IGithubCommitSynchronizationService _githubCommitSynchronizationService;
        private readonly ILogger<GithubSynchronizationController> _logger;

        public GithubSynchronizationController(
            IGithubCommitSynchronizationService githubCommitSynchronizationService,
            ILogger<GithubSynchronizationController> logger)
        {
            _githubCommitSynchronizationService = githubCommitSynchronizationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _githubCommitSynchronizationService.ExecuteSynchronization();
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"{nameof(GithubSynchronizationController)} error.");
                return StatusCode(500, exc.Message);
            }
        }
    }
}