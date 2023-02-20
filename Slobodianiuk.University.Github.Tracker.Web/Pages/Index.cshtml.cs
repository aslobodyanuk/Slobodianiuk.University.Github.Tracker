﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Slobodianiuk.University.Github.Tracker.Core.Services.Frontend;
using Slobodianiuk.University.Github.Tracker.Models.Database.Procedure;
using Slobodianiuk.University.Github.Tracker.Models.Github;
using System.Globalization;

namespace Slobodianiuk.University.Github.Tracker.Web.Pages
{
    public class IndexModel : PageModel
    {
        const int DISPLAY_DAYS = 14;
        const string DATE_FORMAT = "yyyy-MM-dd";

        [FromQuery(Name = "from")]
        public string FromDate { get; set; }

        public IList<RepositoryStats> RepositoryStats { get; set; }

        public IList<DateTime> Dates { get; set; }

        public IList<string> DateStrings { get; set; }


        private readonly ITrackerFrontendService _trackerFrontendService;

        public IndexModel(ITrackerFrontendService trackerFrontendService)
        {
            _trackerFrontendService = trackerFrontendService;
            FromDate ??= "2018-04-18";
        }

        public async Task OnGet()
        {
            var from = DateTime.Parse(FromDate);
            var to = from.AddDays(DISPLAY_DAYS - 1);

            RepositoryStats = (await _trackerFrontendService.LoadAllData(from, to)).ToList();
            Dates = GetDateRange(from, to).ToList();
            DateStrings = Dates.Select(x => x.ToString("ddd, dd MMM", CultureInfo.InvariantCulture)).ToList();
        }

        public GetRepositoryStatsResultItem GetRepoSatsForDate(int repositoryId, DateTime date)
        {
            var repository = RepositoryStats?.FirstOrDefault(x => x.RepositoryId == repositoryId);
            var stats = repository?.DayStats?.FirstOrDefault(x => x.Date.Date == date.Date);

            return stats ?? GetRepositoryStatsResultItem.Default;
        }

        public string GetBadgeColorClass(int modifiedLines)
        {
            if (modifiedLines > 1000)
                return "danger";

            if (modifiedLines > 300)
                return "success";

            if (modifiedLines > 100)
                return "warning";

            return "secondary";
        }

        public string GetPageDate(bool? isPrevious)
        {
            if (isPrevious.HasValue == false)
                return DateTime.Now.ToString(DATE_FORMAT);

            if (isPrevious == true)
                return DateTime.Parse(FromDate)
                            .AddDays(-DISPLAY_DAYS)
                            .ToString(DATE_FORMAT);

            return DateTime.Parse(FromDate)
                            .AddDays(DISPLAY_DAYS)
                            .ToString(DATE_FORMAT);
        }

        private IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            while (startDate <= endDate)
            {
                yield return startDate;
                startDate = startDate.AddDays(1);
            }
        }
    }
}