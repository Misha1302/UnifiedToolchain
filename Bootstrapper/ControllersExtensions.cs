using Microsoft.AspNetCore.Mvc;

namespace Bootstrapper;

public static class ControllersExtensions
{
    public static string GetServiceUri(this Controller controller)
    {
        var req = controller.Request;
        // var locationWithMethod = new Uri($"{req.Scheme}://{req.Host}{req.Path}{req.QueryString}");
        var location = new Uri($"{req.Scheme}://{req.Host}{req.Path}");
        return location.AbsoluteUri;
    }
}