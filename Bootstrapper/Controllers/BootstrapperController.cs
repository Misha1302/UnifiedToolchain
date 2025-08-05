using ExceptionsManager;
using Jsons;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RequestsManager;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Bootstrapper.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BootstrapperController(BootstrapperData data) : Controller
{
    private Json ControllerUriJson => new($$""" { "BootstrapperUri": "{{this.GetServiceUri()}}" } """);

    [HttpPost]
    public Json GetPlugins(Json json) => PluginsCollection.Serialize(data.Plugins);

    [HttpPost]
    public async Task<Json> ImportPlugin(Json json)
    {
        var dynJson = (dynamic)json;

        var plugin = new Plugin(
            (string)dynJson.Name, (string)dynJson.Path, (string)dynJson.Args, (string)dynJson.Uri
        );
        await Services.Send(plugin.UriWithName, "Initialize", ControllerUriJson);
        data.Plugins.Add(plugin);
        return Jsons.Json.Empty;
    }

    [HttpPost]
    public async Task<Json> ImportConfiguration(Json json)
    {
        dynamic configuration = new Json(await System.IO.File.ReadAllTextAsync((string)((dynamic)json).Path));
        var plugins = configuration.Plugins;

        data.PluginsToImport.AddRange(
            ((JArray)plugins).Select(x =>
                JsonSerializer.Deserialize<Plugin>(x.ToString())! with { Uri = $"http://localhost:{data.FreePort++}/" }
            )
        );

        var tasks = data.PluginsToImport.Select(x => PluginsRunner.Instance.Run(x.Path, x.Uri, x.Args));
        Task.WaitAll(tasks.ToArray());

        while (data.PluginsToImport.Count != 0)
        {
            var res = data.PluginsToImport
                .FirstOrDefault(t =>
                    ((dynamic)Services.Send(t.UriWithName, "CanBeInitialized", ControllerUriJson).Result).Success
                );

            Thrower.AssertAlways(
                res != null,
                $"Plugins [{string.Join(", ", data.PluginsToImport)}] were not imported"
            );

            data.PluginsToImport.Remove(res);

            var j = new Json(JsonConvert.SerializeObject(res));
            await ImportPlugin(j);
        }

        return Jsons.Json.Empty;
    }
}