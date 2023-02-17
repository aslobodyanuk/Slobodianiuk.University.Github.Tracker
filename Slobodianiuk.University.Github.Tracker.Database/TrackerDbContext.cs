using Microsoft.EntityFrameworkCore;
using Slobodianiuk.University.Github.Tracker.Database.Views;
using Slobodianiuk.University.Github.Tracker.Models.Database;
using Slobodianiuk.University.Github.Tracker.Models.Database.Procedure;

namespace Slobodianiuk.University.Github.Tracker.Database
{
    public class TrackerDbContext : DbContext
    {
        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }

        public DbSet<GithubUserReference> GithubUserReferences { get; set; }

        public DbSet<Stats> CommitStats { get; set; }

        public DbSet<GetRepositoryStatsResultItem> RepositoryStats { get; set; }

        public TrackerDbContext() { }

        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<GetRepositoryStatsResultItem>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView(RepositoryStatsView.Name);
                    });
        }
    }
}
