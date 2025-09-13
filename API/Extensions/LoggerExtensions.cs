using Serilog;

namespace API.Extensions;

public static class LoggerExtensions
{
    public static void AddLog(this IHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        host.UseSerilog();
    }

    public static void UseLog(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                var ex = feature?.Error;

                if (ex != null)
                {
                    Log.Error(ex, "Unhandled exception occurred at {Path}", feature.Path);
                }

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error" });
            });
        });

        app.UseSerilogRequestLogging();
    }
}