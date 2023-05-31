namespace Slobodianiuk.University.Github.Tracker.Models.Database.Procedure
{
    public class GetRepositoryAllTimeStatsResultItem
    {
        public DateTime Date { get; set; }

        public int Additions { get; set; }

        public int Deletions { get; set; }

        public int Total { get; set; }

        public int FilesCount { get; set; }

        public static GetRepositoryAllTimeStatsResultItem Default = new()
        {
            Additions = 0,
            Deletions = 0,
            Total = 0,
            FilesCount = 0
        };
    }
}
