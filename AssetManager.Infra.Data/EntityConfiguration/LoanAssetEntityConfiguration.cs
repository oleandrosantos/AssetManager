using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class LoanAssetEntityConfiguration : IEntityTypeConfiguration<LoanAssetEntity>
{
    public void Configure(EntityTypeBuilder<LoanAssetEntity> builder)
    {
        builder.HasIndex(a => a.IdLoanAsset);
        builder.Property(a => a.IdLoanAsset).HasMaxLength(32).IsRequired();
        builder.Property(a => a.LoanDate).IsRequired();
        builder.Property(a => a.Description).HasMaxLength(256);
        builder.Property(a => a.IdUser).HasMaxLength(32);
        builder.HasOne(a => a.Asset)
            .WithOne(a => a.LoanAsset)
            .HasForeignKey<LoanAssetEntity>(a => a.IdAsset);

        builder.HasOne(a => a.Company)
            .WithMany(a => a.Loans)
            .HasForeignKey(c => c.IdCompany);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(a => a.IdLoanAsset);

    }
}
