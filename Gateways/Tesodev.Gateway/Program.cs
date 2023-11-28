using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddOcelot();

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

var app = builder.Build();

app.UseAuthorization();

await app.UseOcelot();

app.Run();
