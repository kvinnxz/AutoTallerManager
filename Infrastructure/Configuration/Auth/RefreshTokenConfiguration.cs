using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> b)
    {
        b.ToTable("refresh_tokens");
        b.HasKey(r => r.Id);
        b.Property(r => r.Token).HasMaxLength(500).IsRequired();
        b.Property(r => r.CreatedByIp).HasMaxLength(45);
        b.HasOne(r => r.Usuario).WithMany(u => u.RefreshTokens)
         .HasForeignKey(r => r.UsuarioId).OnDelete(DeleteBehavior.Cascade);
    }
}
