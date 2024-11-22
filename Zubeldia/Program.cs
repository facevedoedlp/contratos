namespace Zubeldia;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Zubeldia.Services.Session;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.InjectionStart(
            builder.Configuration,
            builder.Environment
        );
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var tokenService = new TokenService(builder.Configuration);
        tokenService.ConfigureAuthentication(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}