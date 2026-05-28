using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class RepuestoConfiguration : IEntityTypeConfiguration<Repuesto>
{
    public void Configure(EntityTypeBuilder<Repuesto> b)
    {
        b.ToTable("repuestos");
        b.HasKey(r => r.Id);
        b.Property(r => r.Codigo).HasMaxLength(30).IsRequired();
        b.Property(r => r.Descripcion).HasMaxLength(200).IsRequired();
        b.Property(r => r.Categoria).HasMaxLength(80);
        b.Property(r => r.PrecioUnitario).HasPrecision(14, 2);
        b.HasIndex(r => r.Codigo).IsUnique();
        b.Ignore(r => r.BajoStock);
    }
}
