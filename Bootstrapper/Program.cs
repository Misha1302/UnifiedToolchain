using Bootstrapper.Controllers;
using Jsons;

// var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder([]);

var mvcBuilder = builder.Services.AddControllers();
mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonCustomSerializer()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new BootstrapperData());

var app = builder.Build();
// if (args.Length >= 1) app.Urls.Add(args[0]);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();