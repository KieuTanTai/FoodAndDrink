using ProjectShop.Server.Infrastructure.Configuration;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Thêm cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:58435")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Nếu bạn dùng cookie/session
    });
});

try
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    //builder.Services.AddControllerServices();
    builder.WebHost.UseUrls("https://localhost:5294");
    SnakeCaseMapperInitializer.RegisterAllEntities();
}
catch (Exception ex)
{
    System.Console.WriteLine($"Application startup failed: {ex.Message}");
    Environment.Exit(1);
}

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        // Tuỳ chỉnh thêm nếu muốn
    });

var app = builder.Build();
SqlTypeHandlerRegistration.Register();
GetProviderService.SetServiceProvider(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowLocalhostFrontend");

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
