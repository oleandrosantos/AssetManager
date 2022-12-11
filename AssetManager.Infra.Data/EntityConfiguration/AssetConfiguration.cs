﻿using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetConfiguration : IEntityTypeConfiguration<AssetEntity>
{
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {
        builder.HasKey(a => a.IdAsset);
        builder.Property(a => a.AssetName)
            .HasMaxLength(120);
        builder.HasOne(a => a.Company)
            .WithMany(a => a.Asset)
            .HasForeignKey(a => a.IdCompany);
        builder.HasMany(a => a.AssetEvents)
            .WithOne(a => a.Asset)
            .HasForeignKey(a => a.IdAsset);
    }
}
