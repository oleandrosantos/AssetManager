using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_company",
                columns: table => new
                {
                    idCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    companyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ativa = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_company", x => x.idCompany);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_asset",
                columns: table => new
                {
                    idAsset = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assetName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    depreciationTaxInCents = table.Column<int>(type: "int", nullable: true),
                    assetPriceInCents = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    acquisitionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    exclusionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    exclusionInfos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    companyidCompany = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    decription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_asset", x => x.idAsset);
                    table.ForeignKey(
                        name: "FK_tb_asset_tb_company_companyidCompany",
                        column: x => x.companyidCompany,
                        principalTable: "tb_company",
                        principalColumn: "idCompany",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    idUsuario = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    companyidCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_tb_usuario_tb_company_companyidCompany",
                        column: x => x.companyidCompany,
                        principalTable: "tb_company",
                        principalColumn: "idCompany",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_loanasset",
                columns: table => new
                {
                    idLoanAsset = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    loanDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    devolutionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuarioidUsuario = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assetidAsset = table.Column<int>(type: "int", nullable: false),
                    companyidCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_loanasset", x => x.idLoanAsset);
                    table.ForeignKey(
                        name: "FK_tb_loanasset_tb_asset_assetidAsset",
                        column: x => x.assetidAsset,
                        principalTable: "tb_asset",
                        principalColumn: "idAsset",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_loanasset_tb_company_companyidCompany",
                        column: x => x.companyidCompany,
                        principalTable: "tb_company",
                        principalColumn: "idCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_loanasset_tb_usuario_usuarioidUsuario",
                        column: x => x.usuarioidUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_asset_companyidCompany",
                table: "tb_asset",
                column: "companyidCompany");

            migrationBuilder.CreateIndex(
                name: "IX_tb_loanasset_assetidAsset",
                table: "tb_loanasset",
                column: "assetidAsset");

            migrationBuilder.CreateIndex(
                name: "IX_tb_loanasset_companyidCompany",
                table: "tb_loanasset",
                column: "companyidCompany");

            migrationBuilder.CreateIndex(
                name: "IX_tb_loanasset_usuarioidUsuario",
                table: "tb_loanasset",
                column: "usuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_companyidCompany",
                table: "tb_usuario",
                column: "companyidCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_loanasset");

            migrationBuilder.DropTable(
                name: "tb_asset");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_company");
        }
    }
}
