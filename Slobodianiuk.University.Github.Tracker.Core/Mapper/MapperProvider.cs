using AutoMapper;
using Slobodianiuk.University.Github.Tracker.Core.Interfaces;
using Slobodianiuk.University.Github.Tracker.Models.Database;
using Slobodianiuk.University.Github.Tracker.Models.Github;
using System.Text.Json;

namespace Finance.Formatter.Core.Mapper
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public MapperProvider()
        {
            _mapper = Initialize();
        }

        public IMapper GetMapper() => _mapper;

        private IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Octokit.Repository, Repository>()
                    .ForMember(x => x.Id, m => m.Ignore())
                    .ForMember(x => x.Commits, m => m.Ignore())
                    .ForMember(x => x.Url, m => m.Ignore())
                    .ForMember(x => x.Name, m => m.Ignore())
                    .ForMember(x => x.Surname, m => m.Ignore())
                    .ForMember(x => x.AltName, m => m.Ignore())
                    .ForMember(x => x.Json, m => m.MapFrom(f => SerializeObject(f)));

                cfg.CreateMap<Octokit.GitHubCommit, Commit>()
                    .ForMember(x => x.Id, m => m.Ignore())
                    .ForMember(x => x.SHA, m => m.MapFrom(f => f.Sha))
                    .ForMember(x => x.Message, m => m.MapFrom(f => f.Commit.Message))
                    .ForMember(x => x.Author, m => m.MapFrom(f => f.Commit.Author))
                    .ForMember(x => x.Committer, m => m.MapFrom(f => f.Commit.Committer))
                    .ForMember(x => x.FilesCount, m => m.MapFrom(f => GetCountSafe(f.Files)))
                    .ForMember(x => x.Json, m => m.MapFrom(f => SerializeObject(f)));

                cfg.CreateMap<Octokit.Committer, GithubUserReference>(MemberList.None)
                    .ForMember(x => x.Id, m => m.Ignore())
                    .ForMember(x => x.Date, m => m.MapFrom(f => f.Date))
                    .ForMember(x => x.Email, m => m.MapFrom(f => f.Email))
                    .ForMember(x => x.Name, m => m.MapFrom(f => f.Name));

                cfg.CreateMap<Octokit.GitHubCommitStats, Stats>()
                    .ForMember(x => x.Id, m => m.Ignore())
                    .ForMember(x => x.Additions, m => m.MapFrom(f => f.Additions))
                    .ForMember(x => x.Deletions, m => m.MapFrom(f => f.Deletions))
                    .ForMember(x => x.Total, m => m.MapFrom(f => f.Total));

                cfg.CreateMap<Repository, RepositoryStats>()
                    .ForMember(x => x.RepositoryId, m => m.MapFrom(f => f.Id))
                    .ForMember(x => x.Name, m => m.MapFrom(f => f.Name))
                    .ForMember(x => x.Surname, m => m.MapFrom(f => f.Surname))
                    .ForMember(x => x.AltName, m => m.MapFrom(f => f.AltName))
                    .ForMember(x => x.Url, m => m.MapFrom(f => f.Url))
                    .ForMember(x => x.DayStats, m => m.Ignore());

            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }

        private int GetCountSafe<T>(IEnumerable<T> collection)
        {
            return collection?.Count() ?? 0;
        }

        private string SerializeObject<T>(T item)
        {
            return JsonSerializer.Serialize(item);
        }
    }
}
