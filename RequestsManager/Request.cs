using System.Dynamic;
using ExceptionsManager;
using Jsons;

namespace RequestsManager;

public class Request<T> : DynamicObject
{
    public static readonly dynamic Instance = new Request<T>();

    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        Thrower.AssertAlways(args?.Length >= 1);

        var json = (args[0] as Json ?? (args[0] as string)!).ThrowIfNull();
        result = Services.Send<T>(binder.Name, json).Result;
        return true;
    }
}