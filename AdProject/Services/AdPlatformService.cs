using System;
using System.Text.RegularExpressions;
using AdProject.Collections;
using AdProject.Entities;
using AdProject.Repositories;

namespace AdProject.Services;


public class AdPlatformService : IAdPlatformService
{
    private IAdPlatformRepository _adPlatformRepository;

    public AdPlatformService(IAdPlatformRepository adPlatformRepository)
    {
        _adPlatformRepository = adPlatformRepository;
    }

    public async Task AddPlatforms(string file)
    {
        await _adPlatformRepository.AddPlatforms(ParseFile(file));
    }

    public async Task<IEnumerable<AdPlatform>> FindPlatforms(string location)
    {
        return await _adPlatformRepository.FindPlatforms(location);
    }

    private IEnumerable<AdPlatform> ParseFile(string file)
    {
        var result = new List<AdPlatform>();
        if (string.IsNullOrEmpty(file))
        {
            throw new ArgumentException("file is empty");
        }

        var platforms = file.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        if (platforms.Length == 0)
        {
            throw new ArgumentException("file does not contain any platforms");
        }

        foreach (var platform in platforms)
        {
            var platformNameAndLocations = platform.Trim().Split(":");
            if (platformNameAndLocations.Length != 2 || !Regex.IsMatch(platformNameAndLocations[1], @"^[a-zA-Zа-яА-ЯёЁ\s,/]+$"))
            {
                throw new ArgumentException("invalid format of platform or invalid location");
            }
            var locations = platformNameAndLocations[1].Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                       .Select(s => "/" + string.Join("/", s.Split("/", StringSplitOptions.RemoveEmptyEntries)).Trim())
                                                       .ToList();

            var adPlatform = new AdPlatform(platformNameAndLocations[0].Trim(), locations);
            result.Add(adPlatform);
        }
        return result;
    }




}

