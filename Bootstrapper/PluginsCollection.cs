using System.Collections;
using Jsons;
using Newtonsoft.Json;

namespace Bootstrapper;

public class PluginsCollection(List<Plugin>? plugins = null) : IEnumerable
{
    private readonly List<Plugin> _plugins = plugins ?? [];

    public IEnumerator GetEnumerator() => _plugins.GetEnumerator();

    public void Add(Plugin plugin) => _plugins.Add(plugin);
    public Plugin Get(string name) => _plugins.First(x => x.Name == name);

    public static string Serialize(PluginsCollection obj) =>
        new Json(JsonConvert.SerializeObject(obj._plugins));

    public static PluginsCollection Deserialize(Json json) => new(
        JsonConvert.DeserializeObject<List<Plugin>>(json.ToString())
    );
}