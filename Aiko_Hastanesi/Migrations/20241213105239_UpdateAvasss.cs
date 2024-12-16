using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvasss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availability");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HocaMusaitlikId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Availability_HocaMusaitliks_HocaMusaitlikId",
                        column: x => x.HocaMusaitlikId,
                        principalTable: "HocaMusaitliks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_HocaMusaitlikId",
                table: "Availability",
                column: "HocaMusaitlikId");
        }
    }
}
