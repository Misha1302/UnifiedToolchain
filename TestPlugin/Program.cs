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