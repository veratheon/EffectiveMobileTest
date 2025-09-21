using AdProject.Entities;

namespace AdProject.Repositories
{
    public interface IAdPlatformRepository
    {
        Task AddPlatforms(IEnumerable<AdPlatform> platforms);
        Task<IEnumerable<AdPlatform>> FindPlatforms(string location);
    }
}