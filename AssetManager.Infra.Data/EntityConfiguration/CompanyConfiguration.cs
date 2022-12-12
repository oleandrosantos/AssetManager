using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class CompanyConfiguration : IEntityTypeConfiguration<AssetEventsEntity>
{
    public void Configure(EntityTypeBuilder<AssetEventsEntity> builder)
    {
        builder.ToTable("tb_company");
        builder.HasKey(c => c.IdCompany);
        builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(60);
        builder.Property(c => c.Cnpj).HasMaxLength(14).IsRequired();
        builder.Property(c => c.IsAtiva).HasDefaultValue(true);
    }
}