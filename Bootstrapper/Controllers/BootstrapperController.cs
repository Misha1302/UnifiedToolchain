using System.Reflection;
using CommonExtensions;
using ExceptionsManager;
using Jsons;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PluginIdentifiers;
using Plugins;
using RequestsManager;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Bootstrapper.Controllers;

[ApiController]
[Route("[action]")]
public class BootstrapperController(BootstrapperData data) : Controller
{
    private Json ControllerUriJson => new(new { BootstrapperUri = this.GetServiceUri() });

    [HttpPost]
    public Json GetPlugins(Json json) => PluginsCollection.Serialize(data.Plugins);

    [HttpPost]
    public async Task<Json> ImportPlugin(Json json)
    {
        var dynJson = (dynamic)json;

        var identifier = GetInterfaceIdentifier((string)dynJson.Path);
        var plugin = new Plugin(
            (string)dynJson.Name,
            (string)dynJson.Path,
            (string)dynJson.Args,
            (string)dynJson.Uri,
            identifier
        );

        Services.RegisterServer(identifier, plugin.Uri);
        await Services.Send(identifier, "IsInitialized", ControllerUriJson);
        data.Plugins.Add(plugin);

        return Jsons.Json.Empty;
    }

    private Type GetInterfaceIdentifier(string path)
    {
        path = Path.GetFullPath(path);
        if (!path.EndsWith(".dll")) path += ".dll";
        return Assembly.LoadFile(path).GetTypes()
            .Where(x => x.IsInterface)
            .First(x => x.GetCustomAttributes().Any(y => y is IdentifierAttribute));
    }

    [HttpPost]
    public async Task<Json> ImportConfiguration(Json json)
    {
        var path = (string)((dynamic)json).Path;
        Thrower.AssertAlways(System.IO.File.Exists(path), "Configuration file not found");
        dynamic configuration = new Json(await System.IO.File.ReadAllTextAsync(path));
        var plugins = configuration.Plugins;

        data.PluginsToImport.AddRange(
            ((JArray)plugins).Select(x =>
                JsonSerializer.Deserialize<Plugin>(x.ToString())! with { Uri = $"http://localhost:{data.FreePort++}/" }
            )
        );

        var tasks = data.PluginsToImport
            .Select(x => PluginsRunner.Instance.Run(x.Path, x.Uri, x.Args));
        Task.WaitAll(tasks.ToArray());

        while (!data.PluginsToImport.IsEmpty())
        {
            var res = data.PluginsToImport.FirstOrDefault();

            Thrower.AssertAlways(
                res != null,
                $"Plugins [{string.Join(", ", data.PluginsToImport)}] were not imported"
            );

            data.PluginsToImport.Remove(res);

            await ImportPlugin(new Json(res));
        }

        return Jsons.Json.Empty;
    }
}