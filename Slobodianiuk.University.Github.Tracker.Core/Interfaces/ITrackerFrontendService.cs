using Slobodianiuk.University.Github.Tracker.Models.Github;

namespace Slobodianiuk.University.Github.Tracker.Core.Services.Frontend
{
    public interface ITrackerFrontendService
    {
        Task<IEnumerable<RepositoryStats>> LoadAllData(DateTime from, DateTime to);
    }
}