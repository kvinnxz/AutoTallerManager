using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class OrdenServicioConfiguration : IEntityTypeConfiguration<OrdenServicio>
{
    public void Configure(EntityTypeBuilder<OrdenServicio> b)
    {
        b.ToTable("ordenes_servicio");
        b.HasKey(o => o.Id);
        b.Property(o => o.Descripcion).HasMaxLength(500);
        b.Property(o => o.TrabajoRealizado).HasMaxLength(1000);
        b.Property(o => o.CostoManoObra).HasPrecision(14, 2);
        b.HasOne(o => o.Mecanico).WithMany(u => u.OrdenesAsignadas)
         .HasForeignKey(o => o.MecanicoId).OnDelete(DeleteBehavior.Restrict);
        b.HasMany(o => o.Detalles).WithOne(d => d.OrdenServicio)
         .HasForeignKey(d => d.OrdenServicioId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(o => o.Factura).WithOne(f => f.OrdenServicio)
         .HasForeignKey<Factura>(f => f.OrdenServicioId).OnDelete(DeleteBehavior.Restrict);
    }
}
