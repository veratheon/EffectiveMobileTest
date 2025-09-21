namespace AdProject.Entities
{
    public interface IAdPlatform
    {
        string Name { get; set; }
        IEnumerable<string> Locations { get; set; }
    }
}