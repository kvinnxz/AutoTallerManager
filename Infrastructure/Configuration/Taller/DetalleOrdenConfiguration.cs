using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class DetalleOrdenConfiguration : IEntityTypeConfiguration<DetalleOrden>
{
    public void Configure(EntityTypeBuilder<DetalleOrden> b)
    {
        b.ToTable("detalles_orden");
        b.HasKey(d => d.Id);
        b.Property(d => d.PrecioUnitario).HasPrecision(14, 2);
        b.Ignore(d => d.Subtotal);
        b.HasOne(d => d.Repuesto).WithMany(r => r.Detalles)
         .HasForeignKey(d => d.RepuestoId).OnDelete(DeleteBehavior.Restrict);
    }
}
