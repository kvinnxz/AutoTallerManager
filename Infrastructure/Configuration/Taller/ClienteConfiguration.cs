using Domain.Entities.Taller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Taller;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("clientes");
        b.HasKey(c => c.Id);
        b.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
        b.Property(c => c.TelefonoValue).HasColumnName("telefono").HasMaxLength(20).IsRequired();
        b.Property(c => c.CorreoValue).HasColumnName("correo").HasMaxLength(150).IsRequired();
        b.HasIndex(c => c.CorreoValue).IsUnique();
        b.Ignore(c => c.Telefono);
        b.Ignore(c => c.Correo);
        b.HasMany(c => c.Vehiculos).WithOne(v => v.Cliente)
         .HasForeignKey(v => v.ClienteId).OnDelete(DeleteBehavior.Restrict);
        b.HasMany(c => c.Facturas).WithOne(f => f.Cliente)
         .HasForeignKey(f => f.ClienteId).OnDelete(DeleteBehavior.Restrict);
    }
}
