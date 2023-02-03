﻿// <auto-generated />
using System;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AssetManager.Domain.Entities.AssetEntity", b =>
                {
                    b.Property<int>("IdAsset")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<ulong>("AssetPriceInCents")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("DepreciationTaxInCents")
                        .HasColumnType("int");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdAsset");

                    b.HasIndex("IdCompany");

                    b.ToTable("tb_asset", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AssetEventsEntity", b =>
                {
                    b.Property<int>("IdEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.Property<int>("IdAsset")
                        .HasColumnType("int");

                    b.Property<string>("IdUserRegister")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("UserIdUser")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("IdEvent");

                    b.HasIndex("IdAsset");

                    b.HasIndex("UserIdUser");

                    b.ToTable("tb_asset_events", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.CompanyEntity", b =>
                {
                    b.Property<int>("IdCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<bool>("IsAtiva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.HasKey("IdCompany");

                    b.ToTable("tb_company", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.LoanAssetEntity", b =>
                {
                    b.Property<string>("IdLoanAsset")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("DevolutionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdAsset")
                        .HasColumnType("int");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdLoanAsset");

                    b.HasIndex("IdAsset")
                        .IsUnique();

                    b.HasIndex("IdCompany");

                    b.ToTable("tb_loan_asset", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UserEntity", b =>
                {
                    b.Property<string>("IdUser")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasDefaultValue("Funcionario");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.HasKey("IdUser");

                    b.HasAlternateKey("Email");

                    b.HasIndex("IdCompany");

                    b.ToTable("tb_user", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AssetEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.CompanyEntity", "Company")
                        .WithMany("Asset")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AssetEventsEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.AssetEntity", "Asset")
                        .WithMany("AssetEvents")
                        .HasForeignKey("IdAsset")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserIdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.LoanAssetEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.AssetEntity", "Asset")
                        .WithOne("LoanAsset")
                        .HasForeignKey("AssetManager.Domain.Entities.LoanAssetEntity", "IdAsset")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Domain.Entities.CompanyEntity", "Company")
                        .WithMany("Loans")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Domain.Entities.UserEntity", "User")
                        .WithMany("Loans")
                        .HasForeignKey("IdLoanAsset")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.CompanyEntity", "Company")
                        .WithMany("Users")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AssetEntity", b =>
                {
                    b.Navigation("AssetEvents");

                    b.Navigation("LoanAsset")
                        .IsRequired();
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.CompanyEntity", b =>
                {
                    b.Navigation("Asset");

                    b.Navigation("Loans");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
