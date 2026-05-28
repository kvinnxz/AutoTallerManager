using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> b)
    {
        b.ToTable("roles");
        b.HasKey(r => r.Id);
        b.Property(r => r.Nombre).HasMaxLength(50).IsRequired();
        b.HasIndex(r => r.Nombre).IsUnique();
        b.HasData(
            new Rol { Id = 1, Nombre = "Admin",          Descripcion = "Acceso total" },
            new Rol { Id = 2, Nombre = "Mecanico",       Descripcion = "Mecánico del taller" },
            new Rol { Id = 3, Nombre = "Recepcionista",  Descripcion = "Recepcionista del taller" });
    }
}
