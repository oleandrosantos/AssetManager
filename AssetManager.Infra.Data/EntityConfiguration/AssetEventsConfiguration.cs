using AssetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManager.Infra.Data.EntityConfiguration;
public class AssetEventsConfiguration : IEntityTypeConfiguration<EventosAtivoEntity>
{
    public void Configure(EntityTypeBuilder<EventosAtivoEntity> builder)
    {
        builder.ToTable("tb_eventos_ativo");
        builder.HasKey(a => a.IdEventosAtivo);
        builder.Property(a => a.Descricao).HasMaxLength(120);
        builder.Property(a => a.IdUsuarioRegistro).HasMaxLength(36).IsRequired();
        builder.Property(a => a.IdUsuario).HasMaxLength(36);

        builder.HasOne(a => a.Ativo)
            .WithMany(a => a.EventosAtivo)
            .HasForeignKey(a => a.IdAtivo);

        builder.HasOne(a => a.Usuario)
            .WithMany(a => a.EventosAtivo)
            .HasForeignKey(a => a.IdUsuario);

        builder.HasOne(a => a.UsuarioRegistro)
            .WithMany()
            .HasForeignKey(a => a.IdUsuarioRegistro);
    }
}