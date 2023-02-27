using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetEventsConfiguration : IEntityTypeConfiguration<AssetEventsEntity>
{
    public void Configure(EntityTypeBuilder<AssetEventsEntity> builder)
    {
        builder.ToTable("tb_asset_events");
        builder.HasKey(a => a.IdEvent);
        builder.Property(a => a.Description).HasMaxLength(120);
        builder.Property(a => a.IdUserRegister).HasMaxLength(36).IsRequired();
        builder.Property(a => a.IdUser).HasMaxLength(36);

        builder.HasOne(a => a.Asset)
            .WithMany(a => a.AssetEvents)
            .HasForeignKey(a => a.IdAsset);

        builder.HasOne(a => a.User)
            .WithMany(a => a.AssetEvents)
            .HasForeignKey(a => a.IdUser);

        builder.HasOne(a => a.UserRegister)
            .WithMany()
            .HasForeignKey(a => a.IdUserRegister);
    }
}