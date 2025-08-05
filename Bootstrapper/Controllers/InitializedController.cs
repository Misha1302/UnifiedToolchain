using Jsons;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrapper.Controllers;

[ApiController]
[Route("[controller]")]
public class InitializedController : ControllerBase
{
    [HttpGet]
    public Json IsInitialized() => new("{\"Hello\":123}");
}