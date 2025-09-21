using AdProject.Entities;

namespace AdProject.Services
{
    public interface IAdPlatformService
    {
        Task AddPlatforms(string file);
        Task<IEnumerable<AdPlatform>> FindPlatforms(string location);
    }
}