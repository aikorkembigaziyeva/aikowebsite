using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class AcilTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Konum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcilTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    OluşturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acils_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acils_BolumId",
                table: "Acils",
                column: "BolumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acils");
        }
    }
}
