namespace Bootstrapper;

public record Plugin(string Name, string Path, string Args, string Uri)
{
    public string UriWithName => Uri + Name + "/";
}