using System;
using AdProject.Collections;
using AdProject.Entities;

namespace AdProject.Repositories;

public class AdPlatformRepository : IAdPlatformRepository
{
    private ITree<AdPlatform> _tree;
    private readonly static object _lock = new();

    public AdPlatformRepository(ITree<AdPlatform> tree)
    {
        _tree = tree;
    }

    public async Task AddPlatforms(IEnumerable<AdPlatform> platforms)
    {
        lock (_lock)
        {
            _tree.Clear();
            foreach (var platform in platforms)
            {
                foreach (var location in platform.Locations)
                {
                    _tree.Add(location, platform);
                }
            }
        }
    }

    public async Task<IEnumerable<AdPlatform>> FindPlatforms(string location)
    {
        return _tree.Find(location);
    }
}

