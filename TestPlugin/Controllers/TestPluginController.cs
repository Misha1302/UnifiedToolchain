using Jsons;
using Microsoft.AspNetCore.Mvc;

namespace TestPlugin.Controllers;

[ApiController]
[Route("[action]")]
public class TestPluginController : ControllerBase
{
    // BootstrapperUri - string, uri to bootstrapper
    [HttpPost]
    public Json Initialize(Json json) => Json.Empty;
}