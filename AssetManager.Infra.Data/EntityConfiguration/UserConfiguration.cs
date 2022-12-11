using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.IdUsuario);
        builder.Property(u => u.IdUsuario).HasMaxLength(32);
        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();
        builder.HasAlternateKey(u => u.Email);
        builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Role).HasDefaultValue("Funcionario");
        builder.HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.IdCompany);
        builder.Property(u => u.isActive).HasDefaultValue(true);
    }
}
