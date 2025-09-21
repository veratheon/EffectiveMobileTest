using System;
namespace AdProject.Entities;

public class AdPlatform : IAdPlatform
{
    public string Name { get; set; }
    public IEnumerable<string> Locations { get; set; }

    public AdPlatform(string name, IEnumerable<string> locations)
    {
        Locations = locations;
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not AdPlatform) return false;
        var other = obj as AdPlatform;

        return Name == other.Name && Locations.SequenceEqual(other.Locations);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Locations);
    }
}

