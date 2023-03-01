using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class CompanyConfiguration : IEntityTypeConfiguration<CompanhiaEntity>
{
    public void Configure(EntityTypeBuilder<CompanhiaEntity> builder)
    {
        builder.ToTable("tb_companhia");
        builder.HasKey(c => c.IdCompanhia);
        builder.Property(c => c.NomeCompanhia).IsRequired().HasMaxLength(60);
        builder.Property(c => c.Cnpj).HasMaxLength(14).IsRequired();
        builder.Property(c => c.Ativa).HasDefaultValue(true);
    }
}