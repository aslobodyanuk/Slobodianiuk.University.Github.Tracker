namespace Slobodianiuk.University.Github.Tracker.Models.Database
{
    public class Stats : DbItem
    {
        public int Additions { get; set; }

        public int Deletions { get; set; }

        public int Total { get; set; }
    }
}
