using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    public partial class inicio_projeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_companhia",
                columns: table => new
                {
                    IdCompanhia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCompanhia = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ativa = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    ExclusionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_companhia", x => x.IdCompanhia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_ativos",
                columns: table => new
                {
                    IdAtivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeAtivo = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxaDepreciacao = table.Column<int>(type: "int", nullable: false),
                    PrecoEmCentavos = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataAquisicao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InformacoesExclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCompanhia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ativos", x => x.IdAtivo);
                    table.ForeignKey(
                        name: "FK_tb_ativos_tb_companhia_IdCompanhia",
                        column: x => x.IdCompanhia,
                        principalTable: "tb_companhia",
                        principalColumn: "IdCompanhia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, defaultValue: "Funcionario")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCompanhia = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.IdUsuario);
                    table.UniqueConstraint("AK_tb_usuario_Email", x => x.Email);
                    table.ForeignKey(
                        name: "FK_tb_usuario_tb_companhia_IdCompanhia",
                        column: x => x.IdCompanhia,
                        principalTable: "tb_companhia",
                        principalColumn: "IdCompanhia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_eventos_ativo",
                columns: table => new
                {
                    IdEventosAtivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdAtivo = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUsuarioRegistro = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoEvento = table.Column<int>(type: "int", nullable: false),
                    DataEvento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_eventos_ativo", x => x.IdEventosAtivo);
                    table.ForeignKey(
                        name: "FK_tb_eventos_ativo_tb_ativos_IdAtivo",
                        column: x => x.IdAtivo,
                        principalTable: "tb_ativos",
                        principalColumn: "IdAtivo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_eventos_ativo_tb_usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_eventos_ativo_tb_usuario_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "tb_usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ativos_IdCompanhia",
                table: "tb_ativos",
                column: "IdCompanhia");

            migrationBuilder.CreateIndex(
                name: "IX_tb_eventos_ativo_IdAtivo",
                table: "tb_eventos_ativo",
                column: "IdAtivo");

            migrationBuilder.CreateIndex(
                name: "IX_tb_eventos_ativo_IdUsuario",
                table: "tb_eventos_ativo",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_eventos_ativo_IdUsuarioRegistro",
                table: "tb_eventos_ativo",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_IdCompanhia",
                table: "tb_usuario",
                column: "IdCompanhia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_eventos_ativo");

            migrationBuilder.DropTable(
                name: "tb_ativos");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_companhia");
        }
    }
}
