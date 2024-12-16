using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class Musaitlik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nobets_Asistans_AsistanID",
                table: "Nobets");

            migrationBuilder.CreateTable(
                name: "HocaMusaitliks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HocaID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocaMusaitliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocaMusaitliks_Hocas_HocaID",
                        column: x => x.HocaID,
                        principalTable: "Hocas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocaMusaitliks_HocaID",
                table: "HocaMusaitliks",
                column: "HocaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Nobets_Asistans_AsistanID",
                table: "Nobets",
                column: "AsistanID",
                principalTable: "Asistans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nobets_Asistans_AsistanID",
                table: "Nobets");

            migrationBuilder.DropTable(
                name: "HocaMusaitliks");

            migrationBuilder.AddForeignKey(
                name: "FK_Nobets_Asistans_AsistanID",
                table: "Nobets",
                column: "AsistanID",
                principalTable: "Asistans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
