namespace Zubeldia;

using Zubeldia.Middleware;
using Zubeldia.Services.Session;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("EnableAllCors", builder =>
            {
                builder
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Configurar Servicios
        builder.Services.InjectionStart(
            builder.Configuration,
            builder.Environment
        );
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Configurar Autenticación
        var tokenService = new TokenService(builder.Configuration);
        tokenService.ConfigureAuthentication(builder.Services);

        var app = builder.Build();

        // Middleware
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("EnableAllCors");
        app.UseMiddleware<SessionDataMiddleware>();
        app.UseHttpsRedirection();

        // Rutas
        app.MapControllers();
        app.MapGet("/", () => "API is running");

        app.Run();
    }
}
