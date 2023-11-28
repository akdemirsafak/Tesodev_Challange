using Customer.Domain.DbContext;
using Customer.Domain.Entity;
using Customer.Service.Services;
using Customer.Service.TokenOperations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Jwt;
using Shared.Library.Helper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApiDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
    option => { option.MigrationsAssembly(Assembly.GetAssembly(typeof(ApiDbContext))!.GetName().Name); });
});


builder.Services.AddIdentity<ApiUser, IdentityRole>(
        x => { x.User.RequireUniqueEmail = true; })
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);

var tokenOptions = builder.Configuration.GetSection("ApiTokenOptions").Get<ApiTokenOptions>();

builder.Services.Configure<ApiTokenOptions>(builder.Configuration.GetSection("ApiTokenOptions"));


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
