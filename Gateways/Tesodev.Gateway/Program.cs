using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication()
//    .AddJwtBearer("GatewayAuthenticationScheme", opt => 
//    {
//        opt.Authority = builder.Configuration["IdentityServerURL"];
//        opt.Audience = "resource_gateway";
//        opt.RequireHttpsMetadata = false;
//    });

builder.Services.AddOcelot();

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

var app = builder.Build();

app.UseAuthorization();

await app.UseOcelot();

app.Run();
