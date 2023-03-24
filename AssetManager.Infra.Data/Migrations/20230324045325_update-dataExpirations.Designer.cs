﻿// <auto-generated />
using System;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230324045325_update-dataExpirations")]
    partial class updatedataExpirations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AssetManager.Domain.Entities.AtivoEntity", b =>
                {
                    b.Property<int>("IdAtivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAquisicao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataExclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdCompanhia")
                        .HasColumnType("int");

                    b.Property<string>("InformacoesExclusao")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeAtivo")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<ulong>("PrecoEmCentavos")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TaxaDepreciacao")
                        .HasColumnType("int");

                    b.HasKey("IdAtivo");

                    b.HasIndex("IdCompanhia");

                    b.ToTable("tb_ativos", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.CompanhiaEntity", b =>
                {
                    b.Property<int>("IdCompanhia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime?>("ExclusionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeCompanhia")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("IdCompanhia");

                    b.ToTable("tb_companhia", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.EventosAtivoEntity", b =>
                {
                    b.Property<int>("IdEventosAtivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEvento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<int>("IdAtivo")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("IdUsuarioRegistro")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<int>("TipoEvento")
                        .HasColumnType("int");

                    b.HasKey("IdEventosAtivo");

                    b.HasIndex("IdAtivo");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("IdUsuarioRegistro");

                    b.ToTable("tb_eventos_ativo", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<string>("IdUsuario")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("DataExpiracaoRefreshToken")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("IdCompanhia")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasDefaultValue("Funcionario");

                    b.HasKey("IdUsuario");

                    b.HasAlternateKey("Email");

                    b.HasIndex("IdCompanhia");

                    b.ToTable("tb_usuario", (string)null);
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AtivoEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.CompanhiaEntity", "Companhia")
                        .WithMany("Asset")
                        .HasForeignKey("IdCompanhia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companhia");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.EventosAtivoEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.AtivoEntity", "Ativo")
                        .WithMany("EventosAtivo")
                        .HasForeignKey("IdAtivo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany("EventosAtivo")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManager.Domain.Entities.UsuarioEntity", "UsuarioRegistro")
                        .WithMany()
                        .HasForeignKey("IdUsuarioRegistro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ativo");

                    b.Navigation("Usuario");

                    b.Navigation("UsuarioRegistro");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UsuarioEntity", b =>
                {
                    b.HasOne("AssetManager.Domain.Entities.CompanhiaEntity", "Companhia")
                        .WithMany("Users")
                        .HasForeignKey("IdCompanhia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companhia");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.AtivoEntity", b =>
                {
                    b.Navigation("EventosAtivo");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.CompanhiaEntity", b =>
                {
                    b.Navigation("Asset");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AssetManager.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Navigation("EventosAtivo");
                });
#pragma warning restore 612, 618
        }
    }
}
