using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    public partial class asset_events_adjusting_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_loan_asset");

            migrationBuilder.AlterColumn<string>(
                name: "IdUserRegister",
                table: "tb_asset_events",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdUserRegister",
                table: "tb_asset_events",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_loan_asset",
                columns: table => new
                {
                    IdLoanAsset = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdAsset = table.Column<int>(type: "int", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DevolutionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IdUser = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoanDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_loan_asset", x => x.IdLoanAsset);
                    table.ForeignKey(
                        name: "FK_tb_loan_asset_tb_asset_IdAsset",
                        column: x => x.IdAsset,
                        principalTable: "tb_asset",
                        principalColumn: "IdAsset",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_loan_asset_tb_company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "tb_company",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_loan_asset_tb_user_IdLoanAsset",
                        column: x => x.IdLoanAsset,
                        principalTable: "tb_user",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_loan_asset_IdAsset",
                table: "tb_loan_asset",
                column: "IdAsset",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_loan_asset_IdCompany",
                table: "tb_loan_asset",
                column: "IdCompany");
        }
    }
}
