namespace Slobodianiuk.University.Github.Tracker.Models.Database
{
    public class Repository : GithubDbItem
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
