using Slobodianiuk.University.Github.Tracker.Models.Database.Procedure;

namespace Slobodianiuk.University.Github.Tracker.Models.Github
{
    public class RepositoryStats
    {
        public int RepositoryId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IEnumerable<GetRepositoryStatsResultItem> DayStats { get; set; }
    }
}
