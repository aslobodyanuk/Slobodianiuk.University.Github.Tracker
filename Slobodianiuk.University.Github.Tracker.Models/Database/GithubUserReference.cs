namespace Slobodianiuk.University.Github.Tracker.Models.Database
{
    public class GithubUserReference : DbItem
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
