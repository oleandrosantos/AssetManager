using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    public partial class create_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "companyidCompany",
                table: "tb_usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.AddColumn<int>(
                name: "companyidCompany",
                table: "tb_asset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tb_company",
                columns: table => new
                {
                    idCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    companyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_company", x => x.idCompany);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_companyidCompany",
                table: "tb_usuario",
                column: "companyidCompany");

            migrationBuilder.CreateIndex(
                name: "IX_tb_asset_companyidCompany",
                table: "tb_asset",
                column: "companyidCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_asset_tb_company_companyidCompany",
                table: "tb_asset",
                column: "companyidCompany",
                principalTable: "tb_company",
                principalColumn: "idCompany",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario",
                column: "companyidCompany",
                principalTable: "tb_company",
                principalColumn: "idCompany",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_asset_tb_company_companyidCompany",
                table: "tb_asset");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_company");

            migrationBuilder.DropIndex(
                name: "IX_tb_usuario_companyidCompany",
                table: "tb_usuario");

            migrationBuilder.DropIndex(
                name: "IX_tb_asset_companyidCompany",
                table: "tb_asset");

            migrationBuilder.DropColumn(
                name: "companyidCompany",
                table: "tb_usuario");

            migrationBuilder.DropColumn(
                name: "companyidCompany",
                table: "tb_asset");
        }
    }
}
