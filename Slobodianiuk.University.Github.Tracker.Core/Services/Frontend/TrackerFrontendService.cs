﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Slobodianiuk.University.Github.Tracker.Database;
using Slobodianiuk.University.Github.Tracker.Models.Database.Procedure;
using Slobodianiuk.University.Github.Tracker.Models.Github;

namespace Slobodianiuk.University.Github.Tracker.Core.Services.Frontend
{
    public class TrackerFrontendService : ITrackerFrontendService
    {
        private readonly TrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public TrackerFrontendService(TrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RepositoryStats>> LoadAllData(DateTime from, DateTime to)
        {
            var repositories = await _dbContext.Repositories.ToListAsync();
            var results = new List<RepositoryStats>();

            foreach (var repository in repositories)
            {
                var stats = await _dbContext.RepositoryStats
                    .AsNoTracking()
                    .Where(x => x.RepositoryId == repository.Id && x.Date >= from && x.Date <= to)
                    .ToListAsync();
                
                var mapped = _mapper.Map<RepositoryStats>(repository);
                mapped.DayStats = stats;

                results.Add(mapped);
            }

            return results.OrderBy(x => x.Surname).ThenBy(x => x.Name);
        }

        public async Task<RepositoryStats> GetRepositoryChart(int repositoryId)
        {
            var repository = await _dbContext.Repositories.FindAsync(repositoryId);

            if (repository == default)
                throw new Exception($"Repository with id '{repositoryId}' not found.");

            var stats = await _dbContext.RepositoryStats
                    .AsNoTracking()
                    .Where(x => x.RepositoryId == repositoryId)
                    .ToListAsync();

            var mapped = _mapper.Map<RepositoryStats>(repository);
            mapped.DayStats = stats;

            return mapped;
        }

        public async Task<IEnumerable<GetRepositoryAllTimeStatsResultItem>> GetAllTimeChart()
        {
            return await _dbContext.RepositoryAllTimeStats
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
