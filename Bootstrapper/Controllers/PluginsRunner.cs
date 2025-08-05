using System.Diagnostics;
using ExceptionsManager;

namespace Bootstrapper.Controllers;

public class PluginsRunner
{
    public static readonly PluginsRunner Instance = new();

    public async Task Run(string path, string uri, string args = "")
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = path,
            Arguments = args,
            EnvironmentVariables =
            {
                ["ASPNETCORE_URLS"] = uri,
                ["ASPNETCORE_ENVIRONMENT"] = "Development",
            },
        };

        var process = Process.Start(startInfo);
        process.ThrowIfNull();


        // wait initializing
        while (!process.HasExited)
        {
            try
            {
                var client = new HttpClient();
                var responce = await client.GetAsync(uri + "Initialized");
                if (responce.IsSuccessStatusCode)
                    return;
            }
            catch (HttpRequestException)
            {
            }

            await Task.Delay(100);
        }
    }
}