using System.Data;
using MySql.Data.MySqlClient;

using Back.Modules.LicenceModule.Services;
using Back.Modules.PublicModule.Services;
using Back.Modules.GeneralServices;
using Back.Modules.SubscriptionModule.Services;

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
builder.Services.AddScoped<ITiersService, TiersService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

// Add controllers and OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// builder.Services.AddSwaggerGen(options =>
// {
//     var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
// });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
