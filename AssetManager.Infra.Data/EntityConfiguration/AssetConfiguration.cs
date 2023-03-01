using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetConfiguration : IEntityTypeConfiguration<AtivoEntity>
{
    public void Configure(EntityTypeBuilder<AtivoEntity> builder)
    {
        builder.ToTable("tb_ativos");
        builder.HasKey(a => a.IdAtivo);
        builder.Property(a => a.Sku).HasMaxLength(128);
        builder.Property(a => a.NomeAtivo)
            .HasMaxLength(120);
        builder.HasOne(a => a.Companhia)
            .WithMany(a => a.Asset)
            .HasForeignKey(a => a.IdCompanhia);

        builder.HasMany(a => a.EventosAtivo)
            .WithOne(a => a.Ativo)
            .HasForeignKey(a => a.IdEventosAtivo);
    }
}
