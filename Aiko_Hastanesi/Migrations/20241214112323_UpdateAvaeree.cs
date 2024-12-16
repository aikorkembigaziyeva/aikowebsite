using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvaeree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevus_AspNetUsers_ApplicationUserId",
                table: "Randevus");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Randevus",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevus_AspNetUsers_ApplicationUserId",
                table: "Randevus",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevus_AspNetUsers_ApplicationUserId",
                table: "Randevus");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Randevus",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevus_AspNetUsers_ApplicationUserId",
                table: "Randevus",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
