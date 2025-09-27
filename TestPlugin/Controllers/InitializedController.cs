using Jsons;
using Microsoft.AspNetCore.Mvc;

namespace TestPlugin.Controllers;

[ApiController]
[Route("[action]")]
public class InitializedController : ControllerBase
{
    [HttpPost]
    public Json IsInitialized() => new(new { Hello = 123 });
}