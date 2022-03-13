using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                    exclusionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    exclusionInfos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_asset", x => x.idAsset);
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
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.idUsuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_locationasset",
                columns: table => new
                {
                    idLocationAsset = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    loanDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    devolutionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuarioidUsuario = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    assetidAsset = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_locationasset", x => x.idLocationAsset);
                    table.ForeignKey(
                        name: "FK_tb_locationasset_tb_asset_assetidAsset",
                        column: x => x.assetidAsset,
                        principalTable: "tb_asset",
                        principalColumn: "idAsset",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_locationasset_tb_usuario_usuarioidUsuario",
                        column: x => x.usuarioidUsuario,
                        principalTable: "tb_usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_locationasset_assetidAsset",
                table: "tb_locationasset",
                column: "assetidAsset");

            migrationBuilder.CreateIndex(
                name: "IX_tb_locationasset_usuarioidUsuario",
                table: "tb_locationasset",
                column: "usuarioidUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_locationasset");

            migrationBuilder.DropTable(
                name: "tb_asset");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
