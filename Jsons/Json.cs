using System.Dynamic;
using ExceptionsManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jsons;

public class Json(string text) : DynamicObject
{
    public static readonly Json Empty = new("{}");

    private readonly dynamic _dict = JsonConvert.DeserializeObject(text).ThrowIfNull();

    public override string ToString() => JsonConvert.SerializeObject(_dict, Formatting.None);

    public string ToStringPretty() => JsonConvert.SerializeObject(_dict, Formatting.Indented);

    public static implicit operator Json(string text) => new(text);
    public static implicit operator string(Json json) => json.ToString();

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        _dict[binder.Name] = new JValue(value.ThrowIfNull());
        return true;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        Thrower.AssertAlways(_dict.ContainsKey(binder.Name), $"Invalid member ({binder.Name})");
        result = _dict[binder.Name];
        return true;
    }
}