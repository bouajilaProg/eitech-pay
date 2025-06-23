using System.Data;
using MySql.Data.MySqlClient;

using Back.Modules.LicenceModule.Services;
using Back.Modules.SubscriptionModule.Services;
using Back.Modules.AdminModule.Services;
using Back.Modules.Notification.Services;

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
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ITiersService, TiersService>();
builder.Services.AddScoped<ILicenceService, LicenceService>();
builder.Services.AddScoped<ILicenceOptionService, LicenceOptionService>();
builder.Services.AddScoped<IlicencePublicService, LicencePublicService>();
builder.Services.AddScoped<ISubscriptionPublicService, SubscriptionPublicService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register HttpClient
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (args.Length > 0)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    switch (args[0])
    {
        case "send-email":
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: send-email <email>");
                return;
            }
            var email = args[1];
            var notificationService = services.GetRequiredService<INotificationService>();
            await notificationService.SendNotificationAsync(
                NotificationType.PaymentReminder,
                "banoni.bro@gmail.com",
                new {
                    User = "Ahmed Barhoumi -----",
                    ProductName = "Planetweb Premium ----",
                    DaysLeft = "78787 days +-+",
                    ExpirationDate = "June 26, 2025 -+-+",
                    Email = "ahmed@example.com +-+-----"
                }
            );

            Console.WriteLine("✅ Email sent to " + email);
            return;
    }
}

// ✅ Normal Web Mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();