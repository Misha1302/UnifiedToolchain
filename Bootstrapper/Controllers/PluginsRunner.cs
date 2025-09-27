using System.Diagnostics;
using System.Text;
using ExceptionsManager;

namespace Bootstrapper.Controllers;

public class PluginsRunner
{
    public static readonly PluginsRunner Instance = new();

    private readonly List<Process> _ranProcesses = [];

    public async Task Run(string path, string uri, string args = "", bool redirectOutput = true)
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
            RedirectStandardOutput = redirectOutput,
        };

        var process = Process.Start(startInfo).ThrowIfNull();
        _ranProcesses.Add(process);

        await WaitInitialization(uri, process);
    }

    private static async Task WaitInitialization(string uri, Process process)
    {
        while (!process.HasExited)
        {
            try
            {
                var client = new HttpClient();
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                // Initialized, not IsInitialized, 'cause it's a controller, not a method
                var responce = await client.PostAsync(uri + "IsInitialized", content);
                if (responce.IsSuccessStatusCode)
                    return;
            }
            catch (HttpRequestException)
            {
            }

            await Task.Yield();
        }
    }

    ~PluginsRunner()
    {
        _ranProcesses.ForEach(x => x.Kill());
    }
}