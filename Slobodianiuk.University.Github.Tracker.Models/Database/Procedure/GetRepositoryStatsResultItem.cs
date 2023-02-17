using System.ComponentModel.DataAnnotations.Schema;

namespace Slobodianiuk.University.Github.Tracker.Models.Database.Procedure
{
    public class GetRepositoryStatsResultItem
    {
        public int RepositoryId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Date { get; set; }

        public int Additions { get; set; }

        public int Deletions { get; set; }

        public int Total { get; set; }

        public int FilesCount { get; set; }
    }
}
