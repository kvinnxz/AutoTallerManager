using Api.Extensions;
using Api.Mappings;
using Application;
using AspNetCoreRateLimit;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Configurar Kestrel para desarrollo ──────────────────────────────
builder.WebHost.ConfigureKestrel(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.ListenLocalhost(5000); // HTTP
        options.ListenLocalhost(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
    }
});

// ── Servicios ────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddJwt(builder.Configuration)
    .AddSwaggerWithJwt()
    .AddRateLimiting(builder.Configuration)
    .AddApiServices();

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// ── Mapster config ───────────────────────────────────────────────────
MappingConfig.RegisterMappings();

// ── Pipeline ─────────────────────────────────────────────────────────
var app = builder.Build();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseIpRateLimiting();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoTallerManager v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
