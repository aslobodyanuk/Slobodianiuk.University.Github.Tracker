using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Slobodianiuk.University.Github.Tracker.Core.Constants;
using Slobodianiuk.University.Github.Tracker.Core.Services.Frontend;
using Slobodianiuk.University.Github.Tracker.Models.Frontend;
using Slobodianiuk.University.Github.Tracker.Models.Github;
using System.Globalization;

namespace Slobodianiuk.University.Github.Tracker.Web.Pages
{
    public class ChartModel : PageModel
    {
        [FromQuery(Name = "id")]
        public int RepositoryId { get; set; }

        public RepositoryStats RepositoryStats { get; set; }

        private readonly ITrackerFrontendService _trackerFrontendService;

        public ChartModel(ITrackerFrontendService trackerFrontendService)
        {
            _trackerFrontendService = trackerFrontendService;
        }

        public async Task OnGet()
        {
            RepositoryStats = await _trackerFrontendService.GetRepositoryChart(RepositoryId);
        }

        public decimal GetAverageCodeLinesPerDay()
        {
            return RepositoryStats.DayStats.Sum(x => x.Total) / GetTotalDays();
        }

        public IEnumerable<ChartDataPoint> GetChartValues()
        {
            var totalDays = GetTotalDays();

            var points = Enumerable.Range(0, totalDays + 1)
                .Select(offset => ProjectConstants.CHART_START_DATE.AddDays(offset))
                .Select((y) => {

                    var dayStats = RepositoryStats.DayStats.FirstOrDefault(d => d.Date == y.Date);
                    return new ChartDataPoint()
                    {
                        X = y.ToString(ProjectConstants.USER_READABLE_SHORT_DATE_FORMAT, CultureInfo.InvariantCulture),
                        Y = dayStats?.Total.ToString(),
                        Additions = dayStats?.Additions.ToString(),
                        Deletions = dayStats?.Deletions.ToString()
                    };
                });

            return points.ToArray();
        }

        private int GetTotalDays()
        {
            return ProjectConstants.CHART_END_DATE.Subtract(ProjectConstants.CHART_START_DATE).Days;
        }
    }
}
