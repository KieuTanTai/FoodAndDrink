using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ProjectShop.Server.WebAPI.Middlewares;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseRouting(); // <--- THÊM DÒNG NÀY ĐẦU TIÊN

        app.UseCors("AllowLocalhostFrontend");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            if (env.IsDevelopment())
            {
                endpoints.MapOpenApi();
            }
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/index.html");
        });

        return app;
    }
}