using System.Data;
using MySql.Data.MySqlClient;

using Back.Modules.LicenceModule.Services;
using Back.Modules.PublicModule.Services;
using Back.Modules.GeneralServices;

var builder = WebApplication.CreateBuilder(args);

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Register DatabaseConfig as singleton (contains IConfiguration)
builder.Services.AddSingleton<DatabaseConfig>();

// Register IDbConnection as scoped using GetConnection()
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<DatabaseConfig>();
    return config.GetConnection();
});

// DI for services
builder.Services.AddScoped<ILicenceService, LicenceService>();
builder.Services.AddScoped<ILicenceOptionService, LicenceOptionService>();
builder.Services.AddScoped<IPublicLicenceService, PublicLicenceService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add controllers and OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
