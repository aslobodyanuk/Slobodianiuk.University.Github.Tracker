namespace Slobodianiuk.University.Github.Tracker.Models.Database
{
    public class Commit : GithubDbItem
    {
        public string SHA { get; set; }

        public string Message { get; set; }

        public int FilesCount { get; set; }

        public virtual GithubUserReference? Author { get; set; }

        public virtual GithubUserReference? Committer { get; set; }
        
        public virtual Stats? Stats { get; set; }
    }
}
