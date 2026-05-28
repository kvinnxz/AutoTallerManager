using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> b)
    {
        b.ToTable("usuario_roles");
        b.HasKey(ur => ur.Id);
        b.HasOne(ur => ur.Usuario).WithMany(u => u.UsuarioRoles)
         .HasForeignKey(ur => ur.UsuarioId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(ur => ur.Rol).WithMany(r => r.UsuarioRoles)
         .HasForeignKey(ur => ur.RolId).OnDelete(DeleteBehavior.Restrict);
    }
}
