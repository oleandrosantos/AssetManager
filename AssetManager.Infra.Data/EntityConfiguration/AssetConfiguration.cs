using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetConfiguration : IEntityTypeConfiguration<AssetEntity>
{
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {
        builder.HasKey(a => a.IdAsset);
        builder.Property(a => a.AssetName).HasMaxLength(120);
        builder.Property(a => a.DepreciationTaxInCents);
        builder.Property(a => a.Sku);
        builder.Property(a => a.Status);
        builder.Property(a => a.AssetPriceInCents);
        builder.Property(a => a.AcquisitionDate);
        builder.Property(a => a.)
    }
}
