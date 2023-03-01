using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.ToTable("tb_usuario");
        builder.HasKey(u => u.IdUsuario);
        builder.HasAlternateKey(u => u.Email);
        builder.Property(u => u.IdUsuario).HasMaxLength(36);
        builder.Property(u => u.Nome).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Role).HasDefaultValue("Funcionario").HasMaxLength(32);
        builder.HasOne(u => u.Companhia)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.IdCompanhia);
        builder.Property(u => u.Ativo).HasDefaultValue(true);
    }
}
