using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Migrations
{
    public partial class updateDatabaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "companyidCompany",
                table: "tb_loanasset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "companyName",
                table: "tb_company",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "tb_company",
                type: "varchar(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "exclusionDate",
                table: "tb_asset",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "decription",
                table: "tb_asset",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "tb_asset",
                type: "varchar(64)",
                maxLength: 64,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_loanasset_companyidCompany",
                table: "tb_loanasset",
                column: "companyidCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_loanasset_tb_company_companyidCompany",
                table: "tb_loanasset",
                column: "companyidCompany",
                principalTable: "tb_company",
                principalColumn: "idCompany",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_loanasset_tb_company_companyidCompany",
                table: "tb_loanasset");

            migrationBuilder.DropIndex(
                name: "IX_tb_loanasset_companyidCompany",
                table: "tb_loanasset");

            migrationBuilder.DropColumn(
                name: "companyidCompany",
                table: "tb_loanasset");

            migrationBuilder.DropColumn(
                name: "decription",
                table: "tb_asset");

            migrationBuilder.DropColumn(
                name: "status",
                table: "tb_asset");

            migrationBuilder.UpdateData(
                table: "tb_company",
                keyColumn: "companyName",
                keyValue: null,
                column: "companyName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "companyName",
                table: "tb_company",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "tb_company",
                keyColumn: "cnpj",
                keyValue: null,
                column: "cnpj",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "tb_company",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "exclusionDate",
                table: "tb_asset",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
