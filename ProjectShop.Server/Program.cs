using ProjectShop.Server.Infrastructure.Configuration;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

try
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    SnakeCaseMapperInitializer.RegisterAllEntities();
}
catch (Exception ex)
{
    System.Console.WriteLine($"Application startup failed: {ex.Message}");
    Environment.Exit(1);
}

var app = builder.Build();
SqlTypeHandlerRegistration.Register();
GetProviderService.SetServiceProvider(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
