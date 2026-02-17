namespace API.Extensions;

public static class CORSExtensions
{
    public static void AddCORS(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("vue", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void UseCORS(this IApplicationBuilder app)
    {
        app.UseCors("vue");
    }
}