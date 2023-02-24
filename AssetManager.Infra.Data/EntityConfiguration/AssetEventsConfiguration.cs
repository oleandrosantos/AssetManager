using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetEventsConfiguration : IEntityTypeConfiguration<AssetEventsEntity>
{
    public void Configure(EntityTypeBuilder<AssetEventsEntity> builder)
    {
        builder.ToTable("tb_asset_events");
        builder.HasKey(a => a.IdEvent);
        builder.Property(a => a.Description).HasMaxLength(120);
        builder.Property(a => a.IdUserRegister).HasMaxLength(32).IsRequired();

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