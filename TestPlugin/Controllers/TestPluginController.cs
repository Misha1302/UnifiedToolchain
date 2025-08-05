using Jsons;
using Microsoft.AspNetCore.Mvc;

namespace TestPlugin.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestPluginController : ControllerBase
{
    // BootstrapperUri - string, uri to bootstrapper
    [HttpPost]
    public Json Initialize(Json json) => Json.Empty;

    [HttpPost]
    public Json CanBeInitialized(Json json) =>
        new($$""" { "Success": true, "Uri": "{{((dynamic)json).BootstrapperUri}}" } """);
}