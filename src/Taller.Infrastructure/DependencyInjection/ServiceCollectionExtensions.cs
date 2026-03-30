using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Taller.Application.Abstractions;
using Taller.Application.Siniestros;
using Taller.Infrastructure.Persistence;
using Taller.Infrastructure.Repositories;
using Taller.Infrastructure.Security;

namespace Taller.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? "Server=localhost;Database=TallerDb;Trusted_Connection=True;TrustServerCertificate=True;";

        services.AddDbContext<TallerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ISiniestroRepository, SiniestroRepository>();
        services.AddScoped<IAuditoriaService, AuditoriaService>();
        services.AddScoped<CrearSiniestroUseCase>();
        services.AddScoped<ObtenerSiniestroUseCase>();
        services.AddScoped<JwtTokenService>();

        var key = configuration["Jwt:Key"] ?? "super-secret-key-change-me";
        var issuer = configuration["Jwt:Issuer"] ?? "Taller.API";
        var audience = configuration["Jwt:Audience"] ?? "Taller.Clients";

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("JefeOrAdmin", p => p.RequireRole("JefeTaller", "Admin"));
            options.AddPolicy("OperarioOnly", p => p.RequireRole("Operario"));
        });

        return services;
    }
}
