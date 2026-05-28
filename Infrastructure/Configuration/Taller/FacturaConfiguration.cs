using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
{
    public void Configure(EntityTypeBuilder<Factura> b)
    {
        b.ToTable("facturas");
        b.HasKey(f => f.Id);
        b.Property(f => f.NumeroFactura).HasMaxLength(30).IsRequired();
        b.HasIndex(f => f.NumeroFactura).IsUnique();
        b.Property(f => f.SubtotalRepuestos).HasPrecision(14, 2);
        b.Property(f => f.CostoManoObra).HasPrecision(14, 2);
        b.Property(f => f.Impuesto).HasPrecision(14, 2);
        b.Property(f => f.Total).HasPrecision(14, 2);
    }
}
