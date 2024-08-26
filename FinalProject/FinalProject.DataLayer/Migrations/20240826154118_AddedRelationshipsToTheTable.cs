using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipsToTheTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Adresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfo_UserId",
                table: "UsersInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_UserInfoId",
                table: "Adresses",
                column: "UserInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_UsersInfo_UserInfoId",
                table: "Adresses",
                column: "UserInfoId",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_Users_UserId",
                table: "UsersInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_UsersInfo_UserInfoId",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_Users_UserId",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfo_UserId",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_UserInfoId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Adresses");
        }
    }
}
