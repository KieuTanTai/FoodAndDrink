using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectShop.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhostFrontend", policy =>
            {
                policy.WithOrigins("https://localhost:58435")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

        return services;
    }

    public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
    {
        // Bạn đã dùng builder.Services.AddOpenApi(); nếu là extension bạn tự định nghĩa thì có thể giữ nguyên,
        // còn không thì có thể cấu hình swagger tại đây nếu muốn.
        services.AddOpenApi();
        return services;
    }
}