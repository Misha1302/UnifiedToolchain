namespace Bootstrapper.Controllers;

public class BootstrapperData
{
    public readonly PluginsCollection Plugins = new();
    public readonly List<Plugin> PluginsToImport = [];
    public int FreePort = 5020;
}