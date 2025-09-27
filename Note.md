* every URL should end with '/'
* every plugin should have a controller:

```csharp
[ApiController]
[Route("[action]")]
public class InitializedController : ControllerBase
{
    [HttpPost]
    public Json IsInitialized() => new(new { Hello = 123 });
}
```

* every plugin should have an initializer like this:

```csharp
using Jsons;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddControllers();
mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonCustomSerializer()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

* every plugin should have a controller with its name and with methods:

```csharp
[HttpPost]
public Json Initialize(Json json) => Json.Empty;
```

* relative path should not start with '/' or '\\'