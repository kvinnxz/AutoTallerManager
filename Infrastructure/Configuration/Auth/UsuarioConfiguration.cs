using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> b)
    {
        b.ToTable("usuarios");
        b.HasKey(u => u.Id);
        b.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
        b.Property(u => u.CorreoValue).HasColumnName("correo").HasMaxLength(150).IsRequired();
        b.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
        b.HasIndex(u => u.CorreoValue).IsUnique();
        b.Ignore(u => u.Correo);
    }
}
