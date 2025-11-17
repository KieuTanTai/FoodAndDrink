using ProjectShop.Server.Infrastructure.Configuration;
using ProjectShop.Server.Infrastructure.Services;
using ProjectShop.Server.Extensions;
using ProjectShop.Server.WebAPI.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddCustomAuthentication();

// Cấu hình Serilog log ra file và console
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine(Directory.GetCurrentDirectory(), ".ServerLog", "execute_fulltime.log"),
        rollingInterval: RollingInterval.Infinite,
        shared: true,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
    )
    .MinimumLevel.Debug()
    .CreateLogger();

try
{
    Log.Information("Starting up");
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.WebHost.UseUrls("https://localhost:5294");
    // SnakeCaseMapperInitializer.RegisterAllEntities(); // Removed - no longer needed
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
    Console.WriteLine($"Application startup failed: {ex.Message}");
    Environment.Exit(1);
}

var app = builder.Build();
GetProviderService.SetServiceProvider(app.Services);

app.UseCustomMiddlewares(app.Environment);
app.Run();