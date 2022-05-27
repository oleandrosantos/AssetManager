﻿// <auto-generated />
using System;
using AssetManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManager.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220526041458_adicionando_boleano_usuarioativo")]
    partial class adicionando_boleano_usuarioativo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AssetManager.Model.AssetModel", b =>
                {
                    b.Property<int>("idAsset")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("acquisitionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("assetName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<ulong>("assetPriceInCents")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("companyidCompany")
                        .HasColumnType("int");

                    b.Property<string>("decription")
                        .HasColumnType("longtext");

                    b.Property<int?>("depreciationTaxInCents")
                        .HasColumnType("int");

                    b.Property<DateTime?>("exclusionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("exclusionInfos")
                        .HasColumnType("longtext");

                    b.Property<string>("status")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("idAsset");

                    b.HasIndex("companyidCompany");

                    b.ToTable("tb_asset");
                });

            modelBuilder.Entity("AssetManager.Model.CompanyModel", b =>
                {
                    b.Property<int>("idCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ativa")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("companyName")
                        .HasColumnType("longtext");

                    b.HasKey("idCompany");

                    b.ToTable("tb_company");
                });

            modelBuilder.Entity("AssetManager.Model.LocationAssetModel", b =>
                {
                    b.Property<string>("idLocationAsset")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int>("assetidAsset")
                        .HasColumnType("int");

                    b.Property<int>("companyidCompany")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("devolutionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("loanDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("usuarioidUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("idLocationAsset");

                    b.HasIndex("assetidAsset");

                    b.HasIndex("companyidCompany");

                    b.HasIndex("usuarioidUsuario");

                    b.ToTable("tb_locationasset");
                });

            modelBuilder.Entity("AssetManager.Model.UserModel", b =>
                {
                    b.Property<string>("idUsuario")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int>("companyidCompany")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("isActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idUsuario");

                    b.HasIndex("companyidCompany");

                    b.ToTable("tb_usuario");
                });

            modelBuilder.Entity("AssetManager.Model.AssetModel", b =>
                {
                    b.HasOne("AssetManager.Model.CompanyModel", "company")
                        .WithMany()
                        .HasForeignKey("companyidCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("company");
                });

            modelBuilder.Entity("AssetManager.Model.LocationAssetModel", b =>
                {
                    b.HasOne("AssetManager.Model.AssetModel", "asset")
                        .WithMany()
                        .HasForeignKey("assetidAsset")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Model.CompanyModel", "company")
                        .WithMany()
                        .HasForeignKey("companyidCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Model.UserModel", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioidUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("asset");

                    b.Navigation("company");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("AssetManager.Model.UserModel", b =>
                {
                    b.HasOne("AssetManager.Model.CompanyModel", "company")
                        .WithMany()
                        .HasForeignKey("companyidCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("company");
                });
#pragma warning restore 612, 618
        }
    }
}
