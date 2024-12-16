using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class NobetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nobets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsistanID = table.Column<int>(type: "int", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nobets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nobets_Asistans_AsistanID",
                        column: x => x.AsistanID,
                        principalTable: "Asistans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nobets_Bolums_BolumID",
                        column: x => x.BolumID,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nobets_AsistanID",
                table: "Nobets",
                column: "AsistanID");

            migrationBuilder.CreateIndex(
                name: "IX_Nobets_BolumID",
                table: "Nobets",
                column: "BolumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nobets");
        }
    }
}
