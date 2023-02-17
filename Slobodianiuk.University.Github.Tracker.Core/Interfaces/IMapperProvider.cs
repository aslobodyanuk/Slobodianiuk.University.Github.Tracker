using AutoMapper;

namespace Slobodianiuk.University.Github.Tracker.Core.Interfaces
{
    public interface IMapperProvider
    {
        IMapper GetMapper();
    }
}
