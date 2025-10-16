using ProjectShop.Server.Infrastructure.Configuration;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Services;
using ProjectShop.Server.Extensions;
using ProjectShop.Server.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddCustomAuthentication();

try
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.WebHost.UseUrls("https://localhost:5294");
    // SnakeCaseMapperInitializer.RegisterAllEntities(); // Removed - no longer needed
}
catch (Exception ex)
{
    System.Console.WriteLine($"Application startup failed: {ex.Message}");
    Environment.Exit(1);
}

var app = builder.Build();
SqlTypeHandlerRegistration.Register();
GetProviderService.SetServiceProvider(app.Services);
app.UseCustomMiddlewares(app.Environment);

app.Run();