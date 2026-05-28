using System.Text;
using Api.Services.Auth;
using Application.Abstractions.Auth;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration cfg)
    {
        var key = Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey        = new SymmetricSecurityKey(key),
                    ValidateIssuer          = true, ValidIssuer   = cfg["Jwt:Issuer"],
                    ValidateAudience        = true, ValidAudience = cfg["Jwt:Audience"],
                    ValidateLifetime        = true, ClockSkew     = TimeSpan.Zero
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly",       p => p.RequireRole("Admin"))
            .AddPolicy("MecanicoOAdmin",  p => p.RequireRole("Admin", "Mecanico"))
            .AddPolicy("Autenticado",     p => p.RequireRole("Admin", "Mecanico", "Recepcionista"));

        return services;
    }

    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoTallerManager API", Version = "v1" });
            var scheme = new OpenApiSecurityScheme
            {
                Name = "Authorization", Type = SecuritySchemeType.Http,
                Scheme = "bearer", BearerFormat = "JWT", In = ParameterLocation.Header
            };
            c.AddSecurityDefinition("Bearer", scheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, [] }
            });
        });
        return services;
    }

    public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(cfg.GetSection("IpRateLimiting"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        return services;
    }
}
