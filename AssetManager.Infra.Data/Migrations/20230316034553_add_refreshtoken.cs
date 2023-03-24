using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    public partial class add_refreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracaoRefreshToken",
                table: "tb_usuario",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "tb_usuario",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExpiracaoRefreshToken",
                table: "tb_usuario");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "tb_usuario");
        }
    }
}
