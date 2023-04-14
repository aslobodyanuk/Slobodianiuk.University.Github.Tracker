using System.Text.Json.Serialization;

namespace Slobodianiuk.University.Github.Tracker.Models.Frontend
{
    public class ChartDataPoint
    {
        [JsonPropertyName("x")]
        public string X { get; set; }

        [JsonPropertyName("y")]
        public string Y { get; set; }

        public string Additions { get; set; }

        public string Deletions { get; set; }
    }
}
