using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManager.Infra.Data.Migrations
{
    public partial class asset_events_ajuste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_asset_events_tb_user_UserIdUser",
                table: "tb_asset_events");

            migrationBuilder.RenameColumn(
                name: "UserIdUser",
                table: "tb_asset_events",
                newName: "IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_tb_asset_events_UserIdUser",
                table: "tb_asset_events",
                newName: "IX_tb_asset_events_IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_tb_asset_events_IdUserRegister",
                table: "tb_asset_events",
                column: "IdUserRegister");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_asset_events_tb_user_IdUser",
                table: "tb_asset_events",
                column: "IdUser",
                principalTable: "tb_user",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_asset_events_tb_user_IdUserRegister",
                table: "tb_asset_events",
                column: "IdUserRegister",
                principalTable: "tb_user",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_asset_events_tb_user_IdUser",
                table: "tb_asset_events");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_asset_events_tb_user_IdUserRegister",
                table: "tb_asset_events");

            migrationBuilder.DropIndex(
                name: "IX_tb_asset_events_IdUserRegister",
                table: "tb_asset_events");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "tb_asset_events",
                newName: "UserIdUser");

            migrationBuilder.RenameIndex(
                name: "IX_tb_asset_events_IdUser",
                table: "tb_asset_events",
                newName: "IX_tb_asset_events_UserIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_asset_events_tb_user_UserIdUser",
                table: "tb_asset_events",
                column: "UserIdUser",
                principalTable: "tb_user",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
