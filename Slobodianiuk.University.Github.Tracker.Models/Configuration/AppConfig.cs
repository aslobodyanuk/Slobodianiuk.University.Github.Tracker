namespace Slobodianiuk.University.Github.Tracker.Models.Configuration
{
    public class AppConfig
    {
        public int ForecastAmount { get; set; }

        public string GithubClientName { get; set; }

        public string GithubClientToken { get; set; }

        public int CommitFetchDelayMS { get; set; }
    }
}
