using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    public partial class user_model_company_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario");

            migrationBuilder.AlterColumn<int>(
                name: "companyidCompany",
                table: "tb_usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario",
                column: "companyidCompany",
                principalTable: "tb_company",
                principalColumn: "idCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario");

            migrationBuilder.AlterColumn<int>(
                name: "companyidCompany",
                table: "tb_usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_usuario_tb_company_companyidCompany",
                table: "tb_usuario",
                column: "companyidCompany",
                principalTable: "tb_company",
                principalColumn: "idCompany",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
