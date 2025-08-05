using Bootstrapper.Controllers;
using Jsons;
using RequestsManager;

var uri = "http://localhost:5019/";
await PluginsRunner.Instance.Run(
    "/home/micodiy/RiderProjects/UnifiedToolchain/Bootstrapper/bin/Debug/net8.0/Bootstrapper",
    uri
);

Services.RegisterServer<IBootstrapper>(uri + "Bootstrapper/");

Request<IBootstrapper>.Instance.ImportConfiguration(
    """
    { "Path": "/home/micodiy/RiderProjects/UnifiedToolchain/UnifiedToolchain/Configuration.json" } 
    """
);
var plugins = Request<IBootstrapper>.Instance.GetPlugins(Json.Empty);
Console.WriteLine(plugins);

public interface IBootstrapper;