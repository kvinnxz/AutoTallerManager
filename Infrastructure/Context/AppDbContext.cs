using Domain.Entities.Auth;
using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario>       Usuarios        { get; set; }
    public DbSet<Rol>           Roles           { get; set; }
    public DbSet<UsuarioRol>    UsuarioRoles    { get; set; }
    public DbSet<RefreshToken>  RefreshTokens   { get; set; }
    public DbSet<Cliente>       Clientes        { get; set; }
    public DbSet<Vehiculo>      Vehiculos       { get; set; }
    public DbSet<OrdenServicio> OrdenesServicio { get; set; }
    public DbSet<Repuesto>      Repuestos       { get; set; }
    public DbSet<DetalleOrden>  DetallesOrden   { get; set; }
    public DbSet<Factura>       Facturas        { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
