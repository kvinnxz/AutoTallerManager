using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> b)
    {
        b.ToTable("vehiculos");
        b.HasKey(v => v.Id);
        b.Property(v => v.Marca).HasMaxLength(50).IsRequired();
        b.Property(v => v.Modelo).HasMaxLength(50).IsRequired();
        b.Property(v => v.VinValue).HasColumnName("vin").HasMaxLength(17).IsRequired();
        b.HasIndex(v => v.VinValue).IsUnique();
        b.Ignore(v => v.Vin);
        b.HasMany(v => v.OrdenesServicio).WithOne(o => o.Vehiculo)
         .HasForeignKey(o => o.VehiculoId).OnDelete(DeleteBehavior.Restrict);
    }
}
