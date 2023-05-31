using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Slobodianiuk.University.Github.Tracker.Core.Constants;
using Slobodianiuk.University.Github.Tracker.Core.Services.Frontend;
using Slobodianiuk.University.Github.Tracker.Models.Database.Procedure;
using Slobodianiuk.University.Github.Tracker.Models.Frontend;
using System.Globalization;

namespace Slobodianiuk.University.Github.Tracker.Web.Pages
{
    public class AllTimeChart : PageModel
    {
        [FromQuery(Name = "id")]
        public int RepositoryId { get; set; }

        public IEnumerable<GetRepositoryAllTimeStatsResultItem> RepositoryStats { get; set; }

        private readonly ITrackerFrontendService _trackerFrontendService;

        public AllTimeChart(ITrackerFrontendService trackerFrontendService)
        {
            _trackerFrontendService = trackerFrontendService;
        }

        public async Task OnGet()
        {
            RepositoryStats = await _trackerFrontendService.GetAllTimeChart();
        }

        public decimal GetAverageCodeLinesPerDay()
        {
            return RepositoryStats.Sum(x => x.Total) / GetTotalDays();
        }

        public IEnumerable<ChartDataPoint> GetChartValues()
        {
            var totalDays = GetTotalDays();

            var points = Enumerable.Range(0, totalDays + 1)
                .Select(offset => ProjectConstants.CHART_START_DATE.AddDays(offset))
                .Select((y) => {

                    var dayStats = RepositoryStats.FirstOrDefault(d => d.Date == y.Date);
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
