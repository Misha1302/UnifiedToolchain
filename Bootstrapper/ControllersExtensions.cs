using Microsoft.AspNetCore.Mvc;

namespace Bootstrapper;

public static class ControllersExtensions
{
    public static string GetServiceUri(this Controller controller)
    {
        var req = controller.Request;
        var location = new Uri($"{req.Scheme}://{req.Host}{req.Path}").AbsoluteUri;
        var str = location[..(location.LastIndexOf('/') + 1)];
        return str;
    }
}